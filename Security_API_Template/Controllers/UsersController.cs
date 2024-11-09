using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Security_API_Template.Data.Context;
using Security_API_Template.Data.DTOs;
using Security_API_Template.Data.Entites;
using Security_API_Template.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Security_API_Template.Controllers
{
    [Authorize]

    public class UsersController(DataContext dataContext, ITokenService tokenService) : BaseApiController
    {
        /// <summary>
        /// ///////////////////////////////////Get All Users
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUsers>>> GetUsers()
        {
            return Ok(await dataContext.Users.ToListAsync());
        }

        /// <summary>
        /// /////////////////////////////////Get User By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AppUsers>> GetUser(int id)
        {
            var user = await dataContext.Users.FindAsync(id);
            return user == null ? NotFound() : Ok(user);
        }



        #region Add New User

        /// <summary>
        /// ////////////////////////////////////////From URL
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [HttpPost("AddNewUser/{UserName}/{Password}")]
        public async Task<ActionResult<int>> AddNewUser(string UserName, string Password)
        {
            if (await UserExists(UserName)) { return BadRequest("اسم المستخدم مسجل من قبل..الرجاء اختيار اسم مستخدم جديد"); }
            using var HashPass = new HMACSHA512();
            //var user = new AppUsers
            //{
            //    UserName = UserName.ToLower(),
            //    PasswordHash = HashPass.ComputeHash(Encoding.UTF8.GetBytes(Password)),
            //    PasswordSalt = HashPass.Key
            //};
            //dataContext.Users.Add(user);
            //int Result = await dataContext.SaveChangesAsync();
            //return Ok(Result);
            return Ok();

        }


        /// <summary>
        /// /////////////////////////////////////////From Parameters
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        [HttpPost("AddNewUser")]
        public async Task<ActionResult<int>> AddNewUser2(string UserName, string PassWord)
        {
            if (await UserExists(UserName)) { return BadRequest("اسم المستخدم مسجل من قبل..الرجاء اختيار اسم مستخدم جديد"); }
            using var HashPass = new HMACSHA512();
            //var user = new AppUsers
            //{
            //    UserName = UserName.ToLower(),
            //    PasswordHash = HashPass.ComputeHash(Encoding.UTF8.GetBytes(PassWord)),
            //    PasswordSalt = HashPass.Key
            //};
            //dataContext.Users.Add(user);
            //int Result = await dataContext.SaveChangesAsync();
            //return Ok(Result);
            return Ok();

        }

        /// <summary>
        /// ///////////////////////////////////From Body
        /// </summary>
        /// <param name="newUserDTO"></param>
        /// <returns></returns>
        //[HttpPost("AddNewUser2")]
        //public async Task<ActionResult<UserTokenDTO>> AddNewUser([FromBody] UserDTO newUserDTO)
        //{
        //    return Ok();

        //    //throw new Exception("exception");
        //    //if (await UserExists(newUserDTO.UserName)) { return BadRequest("اسم المستخدم مسجل من قبل..الرجاء اختيار اسم مستخدم جديد"); }
        //    //using var HashPass = new HMACSHA512();

        //    //var user = new AppUsers
        //    //{
        //    //    UserName = newUserDTO.UserName.ToLower(),
        //    //    PasswordHash = HashPass.ComputeHash(Encoding.UTF8.GetBytes(newUserDTO.Password)),
        //    //    PasswordSalt = HashPass.Key
        //    //};
        //    //dataContext.Users.Add(user);
        //    //int Result = await dataContext.SaveChangesAsync();
        //    //return Ok(new UserTokenDTO { UserName = user.UserName, Token = tokenService.CreateToken(user) });
        //}

        private async Task<bool> UserExists(string username)
        {
            return await dataContext.Users.AnyAsync(u => u.UserName == username.ToLower());

        }

        #endregion Add New User

        #region User Login


        /// <summary>
        /// /////////////////////////////////////From URL
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        [HttpPost("Login/{username}/{password}")]
        public async Task<ActionResult<UserTokenDTO>> Login(string username, string password)
        {
            var user = await dataContext.Users.FirstOrDefaultAsync(u => u.UserName == username.ToLower());
            if (user == null) { return Unauthorized("اسم المستخدم خطاء"); }

            using var hashPass = new HMACSHA512(user.PasswordSalt);
            var computeHash = hashPass.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i]) { return Unauthorized("كلمة المرور خطاء"); }
            }
            return Ok(new UserTokenDTO { UserName = user.UserName, Token = tokenService.CreateToken(user) });
        }

        /// <summary>
        /// //////////////////////////////////////////////From Parameters
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        [HttpPost("Login2")]
        public async Task<ActionResult<UserTokenDTO>> Login2(string username, string password)
        {
            var user = await dataContext.Users.FirstOrDefaultAsync(u => u.UserName == username.ToLower());
            if (user == null) { return Unauthorized("اسم المستخدم خطاء"); }

            using var hashPass = new HMACSHA512(user.PasswordSalt);
            var computeHash = hashPass.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i]) { return Unauthorized("كلمة المرور خطاء"); }
            }
            return Ok(new UserTokenDTO { UserName = user.UserName, Token = tokenService.CreateToken(user) });
        }

        /// <summary>
        /// ///////////////////////////////////////////From Body
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<UserTokenDTO>> Login([FromBody] UserDTO userDTO)
        {
            var user = await dataContext.Users.FirstOrDefaultAsync(u => u.UserName == userDTO.UserName.ToLower());
            if (user == null) { return Unauthorized("اسم المستخدم خطاء"); }

            using var hashPass = new HMACSHA512(user.PasswordSalt);
            var computeHash = hashPass.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));

            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i]) { return Unauthorized("كلمة المرور خطاء"); }
            }
            return Ok(new UserTokenDTO { UserName = user.UserName, Token = tokenService.CreateToken(user) }); ;
        }

        #endregion User Login

    }
}
