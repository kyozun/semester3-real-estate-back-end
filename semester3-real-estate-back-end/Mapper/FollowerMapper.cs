using semester4.DTO.Follower;

namespace semester4.Mapper;

public static class FollowerMapper
{
    public static FollowerDto ConvertToFollowerDto(this Follower follower)
    {
        return new FollowerDto
        {
            CreatedAt = follower.CreatedAt
        };
    }

    public static FollowerAndMoreDto ConvertToFollowerAndMoreDto(this Follower follower)
    {
        var dto = new FollowerAndMoreDto
        {
            CreatedAt = follower.CreatedAt
        };


        if (follower.Manga != null) dto.Manga = follower.Manga.ConvertToMangaDto();

        if (follower.User != null) dto.User = follower.User.ConvertToUserDto();

        return dto;
    }

    public static Follower ConvertToFollower(this CreateFollowerDto createFollowerDto)
    {
        return new Follower
        {
            UserId = createFollowerDto.UserId,
            MangaId = createFollowerDto.MangaId
        };
    }
}