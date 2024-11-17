using System.Net;
using System.Runtime.InteropServices;
using semester4.DTO.Author;
using semester4.Helpers.Enums.Include;
using semester4.Helpers.Query;

namespace semester4.Interfaces;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAuthors(AuthorQuery authorQuery);
    Task<Author?> GetAuthorById(string authorId, [Optional] List<AuthorInclude> includes);
    Task<HttpStatusCode> CreateAuthor(Author author);
    Task<HttpStatusCode> UpdateAuthor(UpdateAuthorDto updateAuthorDto);
    Task<HttpStatusCode> DeleteAuthor(List<Guid> authorIds);
    Task<bool> AuthorExistsAsync(string authorId);
}