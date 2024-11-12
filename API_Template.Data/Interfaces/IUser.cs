using API_Template.Data.Data.DTOs;
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
        Task<IEnumerable<MemberDto>> GetUsersAsync();
        Task<MemberDto?> GetUserByIdAsync(int id);
        Task<AppUsers?> GetUserByUserNameAsync(string userName);
        Task<MemberDto?> GetMemberByUserNameAsync(string userName);

        Task<UserTokenDto> LoginAsync(string username, string password, [FromBody] UserDto userDTO);


    }
}
