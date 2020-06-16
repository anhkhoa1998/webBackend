using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using webBackend.Models.Authen;
using webBackend.Models.User;
using webBackend.Services;

namespace webBackend.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
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
        public User Authentication([FromQuery] AuthenModel authenModel)
        {
            return _userService.Authenticate(authenModel);
        }
    }
}
