using DotNet_Db_EF_Audit.Domain.Enums;
using DotNet_Db_EF_Audit.Domain.Interface.Db;
using DotNet_Db_EF_Audit.Domain.Interface.Provider;
using DotNet_Db_EF_Audit.Domain.Seed;
using DotNet_Db_EF_Audit.Infra.Db.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNet_Db_EF_Audit.Infra.Db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            ICurrentSessionProvider currentSessionProvider
            ) : base(options)
        {

            _currentSessionProvider = currentSessionProvider;
        }

        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public ICurrentSessionProvider _currentSessionProvider;

        public DbSet<Author> Authors { get; set; } = default!;
        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<AuditTrail> AuditTrails { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("api");

            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AuditTrailConfiguration());
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            var userId = _currentSessionProvider.GetUserId();

            SetAuditableProperties(userId);

            var auditEntries = HandleAuditingBeforeSaveChanges(userId).ToList();
            if (auditEntries.Count > 0)
            {
                await AuditTrails.AddRangeAsync(auditEntries, cancellationToken);
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        private List<AuditTrail> HandleAuditingBeforeSaveChanges(Guid? userId)
        {
            var auditableEntries = ChangeTracker.Entries<IAuditableEntity>()
                .Where(x => x.State is EntityState.Added or EntityState.Deleted or EntityState.Modified)
                .Select(x => CreateTrailEntry(userId, x))
                .ToList();

            return auditableEntries;
        }

        private static AuditTrail CreateTrailEntry(Guid? userId, EntityEntry<IAuditableEntity> entry)
        {
            var trailEntry = new AuditTrail
            {
                Id = Guid.NewGuid(),
                EntityName = entry.Entity.GetType().Name,
                UserId = userId,
                DateUtc = DateTime.UtcNow
            };

            SetAuditTrailPropertyValues(entry, trailEntry);
            SetAuditTrailNavigationValues(entry, trailEntry);
            SetAuditTrailReferenceValues(entry, trailEntry);

            return trailEntry;
        }

        private void SetAuditableProperties(Guid? userId)
        {
            const string systemSource = "system";
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAtUtc = DateTime.UtcNow;
                        entry.Entity.CreatedBy = userId?.ToString() ?? systemSource;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAtUtc = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = userId?.ToString() ?? systemSource;
                        break;
                }
            }
        }


        private static void SetAuditTrailPropertyValues(EntityEntry entry, AuditTrail trailEntry)
        {
            foreach (var property in entry.Properties.Where(x => !x.IsTemporary))
            {
                if (property.Metadata.IsPrimaryKey())
                {
                    trailEntry.PrimaryKey = property.CurrentValue?.ToString();
                    continue;
                }

                if (property.Metadata.Name.Equals("PasswordHash"))
                {
                    continue;
                }

                SetAuditTrailPropertyValue(entry, trailEntry, property);
            }
        }

        private static void SetAuditTrailPropertyValue(EntityEntry entry, AuditTrail trailEntry, PropertyEntry property)
        {
            var propertyName = property.Metadata.Name;

            switch (entry.State)
            {
                case EntityState.Added:
                    trailEntry.TrailType = TrailTypeIdentifier.Create;
                    trailEntry.NewValues[propertyName] = JsonSerializer.Serialize(property.CurrentValue, _jsonOptions);

                    break;

                case EntityState.Deleted:
                    trailEntry.TrailType = TrailTypeIdentifier.Delete;
                    trailEntry.OldValues[propertyName] = JsonSerializer.Serialize(property.OriginalValue, _jsonOptions);

                    break;

                case EntityState.Modified:
                    if (property.IsModified && (property.OriginalValue is null || !property.OriginalValue.Equals(property.CurrentValue)))
                    {
                        trailEntry.ChangedColumns.Add(propertyName);
                        trailEntry.TrailType = TrailTypeIdentifier.Update;
                        trailEntry.OldValues[propertyName] = JsonSerializer.Serialize(property.OriginalValue, _jsonOptions);
                        trailEntry.NewValues[propertyName] = JsonSerializer.Serialize(property.CurrentValue, _jsonOptions);
                    }

                    break;
            }

            if (trailEntry.ChangedColumns.Count > 0)
            {
                trailEntry.TrailType = TrailTypeIdentifier.Update;
            }
        }

        private static void SetAuditTrailNavigationValues(EntityEntry entry, AuditTrail trailEntry)
        {
            foreach (var navigation in entry.Navigations.Where(x => x.Metadata.IsCollection && x.IsModified))
            {
                if (navigation.CurrentValue is not IEnumerable<object> enumerable)
                {
                    continue;
                }

                var collection = enumerable.ToList();
                if (collection.Count == 0)
                {
                    continue;
                }

                var navigationName = collection.First().GetType().Name;
                trailEntry.ChangedColumns.Add(navigationName);
            }
        }

        private static void SetAuditTrailReferenceValues(EntityEntry entry, AuditTrail trailEntry)
        {
            foreach (var reference in entry.References.Where(x => x.IsModified))
            {
                var referenceName = reference.EntityEntry.Entity.GetType().Name;
                trailEntry.ChangedColumns.Add(referenceName);
            }
        }
    }

}