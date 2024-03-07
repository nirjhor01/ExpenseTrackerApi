using ExpenseTrackerApi.Model;
using ExpenseTrackerApi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExpenseTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class AuthController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IAuthenticationService _services;
        public AuthController(IAuthenticationService services)
        {
            _services = services;
        }

        [HttpGet]
        [Route("LogIn")]
        public async Task<IActionResult> LogIn(string UserName,  string PassWord)
        {
            var res = await _services.UserLogInAsync(UserName, PassWord);
            var tokens = _services.GenerateToken(UserName);
            // return Ok(x);
            //if(res != null)
            // Console.WriteLine(tokens);
            //return Ok(tokens);
            //long one = 1;
            // if(res.Item1 == one)
            // {
            // return Ok(new { info = res.Item1, msg = res.Item2 }) ;
            var s = new  {res, tokens};
            return Ok(new { info = res.Item1, token = tokens });
            // }
            //return Unauthorized();
            //return Ok(new { UserInfo = res.Item1, Message = res.Item2 });

        }




        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUserAsync(UserModel userModel)
        {
            var res = await _services.CreateUserAsync(userModel);
            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        [Route("Authorize")]
        public string ProtectedEndpoint()
        {
            var tokens = _services.GenerateToken("Tokens");
            // User is authorized, return some data
            if (tokens != null)
            {
                return tokens;
            }
            return "Bad Req";
        }

    }
}
