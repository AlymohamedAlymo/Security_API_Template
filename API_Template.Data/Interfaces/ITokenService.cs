using API_Template.Data.DataBase.Entites;

namespace API_Template.Data.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUsers user);

    }
}
