using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Interfaces;

public interface ITokenService
{
    /*Tạo token*/
    public string CreateToken(User user, IList<string> roles);
}