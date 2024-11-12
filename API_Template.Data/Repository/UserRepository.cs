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
        public async Task<MemberDto?> GetUserByIdAsync(int id)
        {
            return  await context.Users
                .Where(x => x.Id == id)
                .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<MemberDto?> GetMemberByUserNameAsync(string userName)
        {

            return await context.Users
                .Where(u => u.UserName == userName.ToLower())
                .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
            
        }

        public async Task<AppUsers?> GetUserByUserNameAsync(string userName)
        {

            return await context.Users.SingleOrDefaultAsync(u => u.UserName == userName.ToLower());

        }


        public async Task<IEnumerable<MemberDto>> GetUsersAsync()
        {
            return await context.Users
                .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
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

        public async Task<UserTokenDto> LoginAsync(string username, string password, [FromBody] UserDto userDTO)
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
            if (user == null) { return new UserTokenDto { UserName = null!, Token = null!, Error = "اسم المستخدم خطاء" }; }

            using var hashPass = new HMACSHA512(user.PasswordSalt);
            var computeHash = hashPass.ComputeHash(Encoding.UTF8.GetBytes(_Password));

            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i])
                {
                    return new UserTokenDto { UserName = null!, Token = null!, Error = "كلمة المرور خطاء" };
                }
            }
            return new UserTokenDto { UserName = user.UserName, Token = tokenService.CreateToken(user), Error = null! };

        }
    }
}
