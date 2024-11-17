using System.Net;
using System.Runtime.InteropServices;
using semester4.DTO.CoverArt;
using semester4.Helpers.Enums.Include;
using semester4.Helpers.Query;

namespace semester4.Interfaces;

public interface ICoverArtRepository
{
    Task<IEnumerable<CoverArt>> GetCoverArts(CoverArtQuery coverArtQuery);
    Task<CoverArt?> GetCoverArtById(string coverArtId, [Optional] List<CoverArtInclude> includes);
    Task<HttpStatusCode> CreateCoverArt(CoverArt coverArt, IFormFile coverArtFile);
    Task<HttpStatusCode> UpdateCoverArt(UpdateCoverArtDto updateCoverArtDto);
    Task<HttpStatusCode> DeleteCoverArt(List<Guid> coverArtIds);
    Task<bool> CoverArtExistsAsync(string coverArtId);
}