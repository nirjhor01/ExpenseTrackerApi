using ExpenseTrackerApi.JWT;
using ExpenseTrackerApi.Model;
using ExpenseTrackerApi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

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
        public readonly IUserServiceRepository _userServiceRepository;
        public readonly IJWTManagerRepository _jwtManagerRepository;
        public AuthController(IAuthenticationService services, IUserServiceRepository userServiceRepository, IJWTManagerRepository jwtManagerRepository)
        {
            _services = services;
            _userServiceRepository = userServiceRepository;
            _jwtManagerRepository = jwtManagerRepository;

        }

        /*[HttpGet]
        [Route("LogIn")]
        public async Task<IActionResult> LogIn(string UserName,  string PassWord)
        {
            var res = await _services.UserLogInAsync(UserName, PassWord);
            var tokens = _services.GenerateToken(UserName);
            string s = "Successfull loggedIn";
            return Ok(JsonSerializer.Serialize(s));
            

        }*/




        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUserAsync(UserModel userModel)
        {
            var res = await _services.CreateUserAsync(userModel);
            return Ok(res.StatusCode);
        }

        /*[HttpGet]
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
        }*/


        [AllowAnonymous]
        [HttpPost]
        [Route("LogIn")]
        public async Task<IActionResult> AuthenticateAsync(UserLogin usersdata)
        {
            var validUser = await _userServiceRepository.IsValidUserAsync(usersdata);

            if (!validUser)
            {
                return Unauthorized("Invalid username or password...");
            }

            var token = _jwtManagerRepository.GenerateToken(usersdata.UserName);

            if (token == null)
            {
                return Unauthorized("Invalid Attemptt");
            }

            UserRefreshTokens obj = new UserRefreshTokens
            {
                RefreshToken = token.RefreshToken,
                UserName = usersdata.UserName,
                ExpirationTime = DateTime.UtcNow.AddMinutes(1)

            };

           await _userServiceRepository.AddUserRefreshTokens(obj);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh-token")]
        public IActionResult Refresh(Tokens token)
        {
            var principal = _jwtManagerRepository.GetPrincipalFromExpiredToken(token.AccessToken);
            var username = principal.Identity?.Name;

            var savedRefreshToken = _userServiceRepository.GetSavedRefreshTokens(username, token.RefreshToken);

            if (savedRefreshToken == null)
            {
                return Unauthorized("Invalid attempt!");
            }


            if (DateTime.UtcNow > savedRefreshToken.ExpirationTime)
            {
                return Unauthorized("Refresh token has expired!");
            }

            if (savedRefreshToken.RefreshToken != token.RefreshToken)
            {
                return Unauthorized("Invalid attempt!");
            }

            var newJwtToken = _jwtManagerRepository.GenerateRefreshToken(username);

            if (newJwtToken == null)
            {
                return Unauthorized("Invalid attempt!");
            }

            UserRefreshTokens obj = new UserRefreshTokens
            {
                RefreshToken = newJwtToken.RefreshToken,
                UserName = username,
                ExpirationTime = DateTime.UtcNow.AddMinutes(1) // Set expiration time
            };

            _userServiceRepository.DeleteUserRefreshTokens(username, token.RefreshToken);
            _userServiceRepository.AddUserRefreshTokens(obj);

            return Ok(newJwtToken);
        }

    }
}

    

