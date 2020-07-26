using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using webBackend.Models.Authen;
using webBackend.Models.User;
using webBackend.Services;

namespace webBackend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserServices _userService;

        public UsersController(UserServices userServices)
        {
            _userService = userServices;
        }

        [HttpPost("create")]
        public async Task<User> Create([FromQuery] UserModel userModel)
        {
            return await _userService.Create(userModel);
        }
        [AllowAnonymous]
        [HttpPost("authen")]
        public User Authentication(AuthenModel authenModel)
        {
            return _userService.Authenticate(authenModel);
        }

        //[HttpGet("list-class")]
        //public async Task<List<string>> GetListClass()
        //{
        //    var userId = string.Empty;
        //    var role = string.Empty;
        //    if (HttpContext.User.Identity is ClaimsIdentity identity)
        //    {
        //        userId = identity.FindFirst(ClaimTypes.Name)?.Value;
        //        role = identity.FindFirst(ClaimTypes.Role)?.Value;
        //    }

        //    var listClass = await _userService.GetListClass(userId);

        //    return listClass;
        //}

        //[HttpGet("user-infor")]
        //public async Task<UserInformation> GetUserInformation()
        //{
        //    var userId = string.Empty;
        //    var role = string.Empty;
        //    if (HttpContext.User.Identity is ClaimsIdentity identity)
        //    {
        //        userId = identity.FindFirst(ClaimTypes.Name)?.Value;
        //        role = identity.FindFirst(ClaimTypes.Role)?.Value;
        //    }

        //    var userInfor = await _userService.GetUserInformation(userId);

        //    return userInfor;
        //}

    }
}
