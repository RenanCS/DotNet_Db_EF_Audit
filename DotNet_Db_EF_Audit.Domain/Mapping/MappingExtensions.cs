using DotNet_Db_EF_Audit.Domain.Dto;
using DotNet_Db_EF_Audit.Domain.Http.Request;
using DotNet_Db_EF_Audit.Domain.Http.Response;
using DotNet_Db_EF_Audit.Domain.Seed;

namespace DotNet_Db_EF_Audit.Domain.Mapping
{
    public static class MappingExtensions
    {

        public static AuthorDto MapToAuthoDto(this UpdateAuthorRequest request, Guid id)
        {
            return new AuthorDto() { Id = id, Name = request.Name };
        }

        public static AuthorDto MapToAuthoDto(this CreateAuthorRequest request)
        {
            return new AuthorDto() { Id = Guid.NewGuid(), Name = request.Name };
        }

        public static BookResponse MapToBookResponse(this BookDto entity)
        {
            return new BookResponse(entity.Id, entity.Title, entity.Year, entity?.Author?.Id);
        }

        public static BookDto MapToBookDto(this Book entity)
        {
            return new BookDto()
            {
                Id = entity.Id,
                Title = entity.Title,
                Year = entity.Year,
                Author = new AuthorDto
                {
                    Id = entity.Author.Id,
                    Name = entity.Author.Name,
                    Books = []
                }
            };
        }

        public static Book MapToBookDb(this BookDto entity)
        {
            return new Book()
            {
                Id = Guid.NewGuid(),
                Title = entity.Title,
                Year = entity.Year,
                AuthorId = entity.Author.Id,
                Author = entity.Author.MapToAuthorDb()
            };
        }

        public static AuthorResponse MapToAuthorResponse(this AuthorDto entity)
        {
            List<BookResponse> booksResponse = entity
                .Books
                .Select(x => x.MapToBookResponse())
                .ToList();

            return new AuthorResponse(entity.Id, entity.Name, booksResponse);
        }

        public static AuthorDto MapToAuthorDto(this Author entity)
        {
            return new AuthorDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Books = entity.Books.Select(x => new BookDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Year = x.Year,
                    Author = null!
                }).ToList()
            };
        }

        public static Author MapToAuthorDb(this AuthorDto entity)
        {
            return new Author()
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }

        public static User MapToUserDb(this UserDto entity)
        {
            return new User()
            {
                Id = entity.Id,
                Email = entity.Email,
            };
        }

        public static UserDto MapToUserDto(this User entity)
        {
            return new UserDto()
            {
                Id = entity.Id,
                Email = entity.Email
            };
        }
    }
}
