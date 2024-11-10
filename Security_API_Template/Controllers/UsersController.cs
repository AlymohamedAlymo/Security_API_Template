using API_Template.Data.Data.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Security_API_Template.Data.Context;
using Security_API_Template.Data.DTOs;
using Security_API_Template.Data.Entites;
using Security_API_Template.Interfaces;
using Security_API_Template.Repository;
using System.Security.Cryptography;
using System.Text;

namespace Security_API_Template.Controllers
{
    [Authorize]

    public class UsersController(IUser _Iuser, IMapper mapper) : BaseApiController
    {
        /// <summary>
        /// ///////////////////////////////////Get All Users
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
        {
            var users = await _Iuser.GetUsersAsync();
            var userReterned = mapper.Map<MemberDTO>(users);
            return Ok(userReterned);
        }

        /// <summary>
        /// /////////////////////////////////Get User By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MemberDTO>> GetUserById(int id)
        {
            var user = await _Iuser.GetUserByIdAsync(id);
            var userReterned = mapper.Map<MemberDTO>(user);
            return user == null ? NotFound() : userReterned;
        }

        /// <summary>
        /// /////////////////////////////////Get User By UserName
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [HttpGet("{UserName}")]
        public async Task<ActionResult<MemberDTO>> GetUserByUserName(string UserName)
        {
            var user = await _Iuser.GetUserByUserNameAsync(UserName);
            var userReterned = mapper.Map<MemberDTO>(user);
            return user == null ? NotFound() : userReterned;
        }



        #region Add New User

        /// <summary>
        /// ////////////////////////////////////////From URL
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        //[HttpPost("AddNewUser/{UserName}/{Password}")]
        //public async Task<ActionResult<int>> AddNewUser(string UserName, string Password)
        //{
        //    if (await UserExists(UserName)) { return BadRequest("اسم المستخدم مسجل من قبل..الرجاء اختيار اسم مستخدم جديد"); }
        //    using var HashPass = new HMACSHA512();
        //    //var user = new AppUsers
        //    //{
        //    //    UserName = UserName.ToLower(),
        //    //    PasswordHash = HashPass.ComputeHash(Encoding.UTF8.GetBytes(Password)),
        //    //    PasswordSalt = HashPass.Key
        //    //};
        //    //dataContext.Users.Add(user);
        //    //int Result = await dataContext.SaveChangesAsync();
        //    //return Ok(Result);
        //    return Ok();

        //}


        /// <summary>
        /// /////////////////////////////////////////From Parameters
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        //[HttpPost("AddNewUser")]
        //public async Task<ActionResult<int>> AddNewUser2(string UserName, string PassWord)
        //{
        //    if (await UserExists(UserName)) { return BadRequest("اسم المستخدم مسجل من قبل..الرجاء اختيار اسم مستخدم جديد"); }
        //    using var HashPass = new HMACSHA512();
        //    //var user = new AppUsers
        //    //{
        //    //    UserName = UserName.ToLower(),
        //    //    PasswordHash = HashPass.ComputeHash(Encoding.UTF8.GetBytes(PassWord)),
        //    //    PasswordSalt = HashPass.Key
        //    //};
        //    //dataContext.Users.Add(user);
        //    //int Result = await dataContext.SaveChangesAsync();
        //    //return Ok(Result);
        //    return Ok();

        //}

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

        //private async Task<bool> UserExists(string username)
        //{
        //    return await dataContext.Users.AnyAsync(u => u.UserName == username.ToLower());

        //}

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
        public async Task<ActionResult<UserTokenDTO>> Login(string username, string password, [FromBody] UserDTO userDTO)
        {
            var user = await _Iuser.LoginAsync(username, password, userDTO);
            return user.UserName == null? Unauthorized(user.Error) : user;

        }

        #endregion User Login

    }
}
