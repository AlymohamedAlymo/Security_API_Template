using Security_API_Template.Data.Entites;

namespace Security_API_Template.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUsers user);

    }
}
