using System.Net;
using System.Runtime.InteropServices;
using semester4.DTO.Genre;
using semester4.Helpers.Enums.Include;
using semester4.Helpers.Query;

namespace semester4.Interfaces;

public interface IGenreRepository
{
    Task<IEnumerable<Genre>> GetGenres(GenreQuery genreQuery);
    Task<Genre?> GetGenreById(string genreId, [Optional] List<GenreInclude> includes);
    Task<HttpStatusCode> CreateGenre(Genre genre);
    Task<HttpStatusCode> UpdateGenre(UpdateGenreDto updateGenreDto);
    Task<HttpStatusCode> DeleteGenre(List<Guid> genreIds);
    Task<bool> GenreExistsAsync(string genreId);
}