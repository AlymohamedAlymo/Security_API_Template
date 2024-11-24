using Microsoft.AspNetCore.Mvc;
using API_Template.Data.ViewModels;
using API_Template.Data.DataBase.Entites;
using System.Text;

namespace API_Template.Data.Interfaces
{
    public interface IUser
    {
        void UpdateUser(AppUsers users);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<MemberDto>> GetUsersAsync();
        Task<MemberDto?> GetUserByIdAsync(int id);
        Task<AppUsers?> GetUserByUserNameAsync(string userName);
        Task<MemberDto?> GetMemberByUserNameAsync(string userName);

        Task<UserTokenVM> LoginAsync(string username, string password, [FromBody] UserVM userDTO);


    }
}
