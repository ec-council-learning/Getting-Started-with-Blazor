using Microsoft.AspNetCore.Mvc;
using MovieApp.Server.Interfaces;
using MovieApp.Server.Models;
using MovieApp.Shared.Dto;
using MovieApp.Shared.Models;
using System.Threading.Tasks;

namespace MovieApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUser _userService;

        public UserController(IUser userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserRegistration registrationData)
        {
            UserMaster user = new UserMaster()
            {
                FirstName = registrationData.FirstName,
                LastName = registrationData.LastName,
                Username = registrationData.Username,
                Password = registrationData.Password,
                Gender = registrationData.Gender,
                UserTypeName = UserRoles.User
            };

            bool userRegistrationStatus = await Task.Run(() => _userService.RegisterUser(user));

            if (userRegistrationStatus)
            {
                return Ok(ModelState);
            }
            else
            {
                ModelState.AddModelError(nameof(registrationData.Username), "This User Name is not available.");
                return BadRequest(ModelState);
            }
        }

        [HttpGet("getcurrentuser")]
        public async Task<ActionResult<UserLogin>> GetCurrentUser()
        {
            UserLogin currentUser = new();

            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                currentUser = _userService.GetCurrentUser(userName);
            }

            return await Task.FromResult(currentUser);
        }
    }
}
