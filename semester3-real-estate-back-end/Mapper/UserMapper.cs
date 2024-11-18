using semester3_real_estate_back_end.DTO.User;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Mapper;

public static class UserMapper
{
    public static UserDto ConvertToUserDto(this User user)
    {
            return new UserDto
            {
                UserId = user.Id,
                UserName = user.UserName!,
                Email = user.Email!
            };

    }
}