using System.Net;
using System.Runtime.InteropServices;
using semester4.DTO.Manga;
using semester4.Helpers.Enums.Include;
using semester4.Helpers.Query;

namespace semester4.Interfaces;

public interface IMangaRepository
{
    Task<IEnumerable<Manga>> GetMangas(MangaQuery mangaQuery);
    Task<Manga?> GetMangaById(string mangaId, [Optional] List<MangaInclude> includes);
    Task<HttpStatusCode> CreateManga(Manga manga);
    Task<HttpStatusCode> UpdateManga(UpdateMangaDto updateMangaDto);
    Task<HttpStatusCode> DeleteManga(List<Guid> mangaIds);


    Task<HttpStatusCode> FollowManga(string mangaId);
    Task<HttpStatusCode> UnFollowManga(string mangaId);

    Task<IEnumerable<Manga>?> GetRelationMangas(string mangaId, MangaQuery mangaQuery);
    Task<Manga?> GetRandomManga(MangaQuery mangaQuery);
    Task<bool> MangaExistsAsync(string mangaId);
}