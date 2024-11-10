using API_Template.Data.Data.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Security_API_Template.Data.Context;
using Security_API_Template.Data.DTOs;
using Security_API_Template.Data.Entites;
using Security_API_Template.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Security_API_Template.Repository
{
    public class UserRepository(DataContext context, ITokenService tokenService, IMapper mapper) : IUser
    {
        public async Task<MemberDTO?> GetUserByIdAsync(int id)
        {
            return  await context.Users
                .Where(x => x.Id == id)
                .ProjectTo<MemberDTO>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<MemberDTO?> GetUserByUserNameAsync(string userName)
        {

            return await context.Users
                .Where(u => u.UserName == userName)
                .ProjectTo<MemberDTO>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
            
        }

        public async Task<IEnumerable<MemberDTO>> GetUsersAsync()
        {
            return await context.Users
                .ProjectTo<MemberDTO>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void UpdateUser(AppUsers users)
        {
            context.Entry(users).State = EntityState.Modified;
        }

        public async Task<UserTokenDTO> LoginAsync(string username, string password, [FromBody] UserDTO userDTO)
        {
            var _UserName = string.Empty;
            var _Password = string.Empty;

            if (userDTO == null)
            {
                _UserName = username.ToLower();
                _Password = password;
            }
            else
            {
                _UserName = userDTO.UserName.ToLower();
                _Password = userDTO.Password;
            }

            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == _UserName);
            if (user == null) { return new UserTokenDTO { UserName = null!, Token = null!, Error = "اسم المستخدم خطاء" }; }

            using var hashPass = new HMACSHA512(user.PasswordSalt);
            var computeHash = hashPass.ComputeHash(Encoding.UTF8.GetBytes(_Password));

            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i])
                {
                    return new UserTokenDTO { UserName = null!, Token = null!, Error = "كلمة المرور خطاء" };
                }
            }
            return new UserTokenDTO { UserName = user.UserName, Token = tokenService.CreateToken(user), Error = null! };

        }
    }
}
