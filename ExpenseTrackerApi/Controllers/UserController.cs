using ExpenseTrackerApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ExpenseTrackerApi.Service;
using ExpenseTrackerApi.Service.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IExpeneseTrackerServices _services;
        public UserController(IExpeneseTrackerServices services)
        {
            _services = services;
        }

        [HttpGet("Admins")]
        [Authorize]
        public string AdminsEndPoint()
        {
           // var currentUser = GetCurrentUser();
           // return Ok($"HI {currentUser.UserName},you are an{currentUser.Role}");
           return "401";
        }

        [HttpGet("Public")]
       public IActionResult Public()
        {
            return Ok("Hi ");
        }
        /*/
       private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserModel
                {
                    UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                };

            }
            else return null;
        }
        */




        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUserAsync(UserModel userModel)
        {
            var res = await _services.CreateUserAsync(userModel);
            return Ok(res);
        }

        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> LogIn([MaxLength(30)] string UserName, [MaxLength(30)] string PassWord)
        {
            var res = await _services.LoginUserAsync(UserName, PassWord);
            return Ok(new { UserInfo = res.Item1, Message = res.Item2 });
        }



    }
}
