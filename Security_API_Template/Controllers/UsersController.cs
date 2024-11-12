using API_Template.Data.Data.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security_API_Template.Data.DTOs;
using Security_API_Template.Data.Entites;
using Security_API_Template.Interfaces;
using Security_API_Template.Repository;
using System.Security.Claims;

namespace Security_API_Template.Controllers
{
    [Authorize]

    public class UsersController(IUser _IUser, IMapper mapper) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _IUser.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MemberDto>> GetUserById(int id)
        {
            var user = await _IUser.GetUserByIdAsync(id);
            return user == null ? NotFound() : user;
        }

        [HttpGet("{UserName}")]
        public async Task<ActionResult<MemberDto>> GetUserByUserName(string UserName)
        {
            var user = await _IUser.GetMemberByUserNameAsync(UserName);
            return user == null ? NotFound() : user;
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


        [AllowAnonymous]
        [HttpPost("Login/{username}/{password}")]
        public async Task<ActionResult<UserTokenDto>> Login(string username, string password, [FromBody] UserDto userDTO)
        {
            var user = await _IUser.LoginAsync(username, password, userDTO);
            return user.UserName == null? Unauthorized(user.Error) : user;

        }

        #endregion User Login


        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userName == null) { return BadRequest("لم يتم العثور على اسم المستخدم في الرمز"); }

            var user = await _IUser.GetUserByUserNameAsync(userName);

            if (user == null) { return BadRequest("لم يتم العثور على المستخدم"); }

            mapper.Map(memberUpdateDto, user);

            if (await _IUser.SaveAllAsync()) { return NoContent(); }

            return BadRequest("فشلت عملية تعديل المستخدم");
        }

    }
}
