using semester3_real_estate_back_end.DTO.User;

namespace semester4.Mapper;

public static class UserMapper
{
    public static UserDto? ConvertToUserDto(this User user)
    {
        if (user.UserName != null && user.Email != null)
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };

        return null;
    }
}