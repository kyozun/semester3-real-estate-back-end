using semester4.DTO.CoverArt;

namespace semester4.Mapper;

public static class CoverArtMapper
{
    public static CoverArtDto? ConvertToCoverArtDto(this CoverArt? coverArt)
    {
        if (coverArt == null) return null;
        return new CoverArtDto
        {
            CoverArtId = coverArt.CoverArtId,
            FileName = coverArt.FileName,
            Description = coverArt.Description,
            Locale = coverArt.Locale,
            Volume = coverArt.Volume,
            CreatedAt = coverArt.CreatedAt,
            UpdatedAt = coverArt.UpdatedAt
        };
    }

    public static CoverArtAndMoreDto ConvertToCoverArtAndMoreDto(this CoverArt coverArt)
    {
        var dto = new CoverArtAndMoreDto
        {
            CoverArtId = coverArt.CoverArtId,
            FileName = coverArt.FileName,
            Description = coverArt.Description,
            Locale = coverArt.Locale,
            Volume = coverArt.Volume,
            CreatedAt = coverArt.CreatedAt,
            UpdatedAt = coverArt.UpdatedAt
        };

        if (coverArt.Manga != null) dto.Manga = coverArt.Manga.ConvertToMangaDto();

        if (coverArt.User != null) dto.User = coverArt.User.ConvertToUserDto();

        return dto;
    }

    public static CoverArt ConvertToCoverArt(this CreateCoverArtDto createCoverArtDto)
    {
        return new CoverArt
        {
            CoverArtId = Guid.NewGuid().ToString(),
            Description = createCoverArtDto.Description,
            Locale = createCoverArtDto.Locale,
            Volume = createCoverArtDto.Volume,
            MangaId = createCoverArtDto.MangaId.ToString()!
        };
    }
}