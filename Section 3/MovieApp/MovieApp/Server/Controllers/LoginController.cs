using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Server.Interfaces;
using MovieApp.Shared.Dto;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        readonly IUser _userService;

        public LoginController(IUser userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserLogin login)
        {
            UserLogin user = _userService.AuthenticateUser(login);

            if (!string.IsNullOrEmpty(user.Username))
            {
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("userId", user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.UserTypeName),
                };

                var claimsIdentity = new ClaimsIdentity(userClaims, "BlazorServerAuth");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);

                return Ok(ModelState);
            }
            else
            {
                ModelState.AddModelError(nameof(login.UserId), "Username or Password is incorrect.");
                return BadRequest(ModelState);
            }
        }

        [HttpGet("logoutuser")]
        public async Task<ActionResult<string>> LogOutUser()
        {
            await HttpContext.SignOutAsync();
            return Ok("Success");
        }
    }
}
