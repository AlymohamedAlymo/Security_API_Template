using Microsoft.AspNetCore.Mvc;
using Security_API_Template.Data.DTOs;
using Security_API_Template.Data.Entites;
using System.Text;

namespace Security_API_Template.Interfaces
{
    public interface IUser
    {
        void UpdateUser(AppUsers users);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUsers>> GetUsersAsync();
        Task<AppUsers?> GetUserByIdAsync(int id);
        Task<AppUsers?> GetUserByUserNameAsync(string userName);
        Task<UserTokenDTO> LoginAsync(string username, string password, [FromBody] UserDTO userDTO);


    }
}
