using System.Net;
using System.Runtime.InteropServices;
using semester4.DTO.UserManga;
using semester4.Helpers.Enums.Include;
using semester4.Helpers.Query;

namespace semester4.Interfaces;

public interface IUserMangaRepository
{
    Task<IEnumerable<UserManga>> GetUserMangas(UserMangaQuery userMangaQuery);
    Task<UserManga?> GetUserMangaById(string userMangaId, [Optional] List<UserMangaInclude> includes);
    Task<HttpStatusCode> CreateUserManga(UserManga userManga);
    Task<HttpStatusCode> UpdateUserManga(UpdateUserMangaDto updateUserMangaDto);
    Task<HttpStatusCode> DeleteUserManga(List<Guid> userMangaIds);
    Task<bool> UserMangaExistsAsync(string userMangaId);

    //
    Task<IEnumerable<UserManga>> GetMangaReadingStatus(MangaReadingStatusQuery mangaReadingStatusQuery);

    Task<HttpStatusCode> UpdateMangaReadingStatus(string mangaId,
        UpdateMangaReadingStatusDto updateMangaReadingStatusDto);

    // Follow
    Task<List<Manga>> GetMangaFollowedByUser(MangaFollowedByUserQuery mangaFollowedByUserQuery);
}