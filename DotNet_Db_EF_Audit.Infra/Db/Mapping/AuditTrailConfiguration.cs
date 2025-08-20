using DotNet_Db_EF_Audit.Domain.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace DotNet_Db_EF_Audit.Infra.Db.Mapping
{
    public class AuditTrailConfiguration : IEntityTypeConfiguration<AuditTrail>
    {
        public void Configure(EntityTypeBuilder<AuditTrail> builder)
        {
            builder.ToTable("audit_trails", "api");
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.EntityName);

            builder.Property(e => e.Id);

            builder.Property(e => e.UserId);
            builder.Property(e => e.EntityName).HasMaxLength(100).IsRequired();
            builder.Property(e => e.DateUtc).IsRequired();
            builder.Property(e => e.PrimaryKey).HasMaxLength(100);
            builder.Property(e => e.TrailType).HasConversion<string>();

            // ------ TO POSTEGRES ----
            //builder.Property(e => e.ChangedColumns).HasColumnType("jsonb");
            //builder.Property(e => e.OldValues).HasColumnType("jsonb");
            //builder.Property(e => e.NewValues).HasColumnType("jsonb");

            // ------ TO SQL SERVER ----
            //builder.Property(e => e.ChangedColumns).HasColumnType("text");
            //builder.Property(e => e.OldValues)
            //    .HasConversion(v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
            //                    v => JsonSerializer.Deserialize<Dictionary<string, object?>>(v, new JsonSerializerOptions()))
            //    .HasColumnType("nvarchar(max)");
            //builder.Property(e => e.NewValues)
            //    .HasConversion(v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
            //                    v => JsonSerializer.Deserialize<Dictionary<string, object?>>(v, new JsonSerializerOptions()))
            //    .HasColumnType("nvarchar(max)");


            // ------ TO MYSQL ----
            builder.Property(e => e.ChangedColumns).HasColumnType("text");
            builder.Property(e => e.OldValues)
                .HasConversion(v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                                v => JsonSerializer.Deserialize<Dictionary<string, object?>>(v, new JsonSerializerOptions()))
                .HasColumnType("longtext");
            builder.Property(e => e.NewValues)
                .HasConversion(v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                                v => JsonSerializer.Deserialize<Dictionary<string, object?>>(v, new JsonSerializerOptions()))
                .HasColumnType("longtext");

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}