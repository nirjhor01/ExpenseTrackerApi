using ExpenseTrackerApi.Helper;
using ExpenseTrackerApi.Model;
using ExpenseTrackerApi.Repository.Interfaces;
using ExpenseTrackerApi.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseTrackerApi.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService 
    {

        private readonly IConfiguration _configuration;
        private readonly IExpenseTrackerRepository _repository;

        public AuthenticationService(IConfiguration configuration, IExpenseTrackerRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
        }





        public string GenerateToken(string Username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
                {
                 new Claim(ClaimTypes.NameIdentifier, Username),
                

             };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<(UserLogin,MessageHelperModel)> UserLogInAsync(string UserName, string PassWord)
        {

            var user = await _repository.UserLogInAsync(UserName, PassWord);
            var msg = new MessageHelperModel { Message = "", StatusCode = 200 };
            /*
                        if (user == null)
                        {
                            msg.Message = "Invalid UserName or Password";
                        }
                        else
                        {
                            if (user.Password == PassWord)
                            {
                                msg.Message = "Welcome";

                                //LoginController loginController = new LoginController(_configuration);
                                // msg.Token = loginController.Generate(UserModel);
                            }
                            else
                            {
                                msg.Message = "Invalid Password";
                            }
                            user.Password = null;
                        }*/
            if (user != null && user.UserName != UserName)
            {
                msg.Message = "Invalid Username";
            }
            else if (user != null && user.UserName != UserName && user.Password != PassWord) msg.Message = "Unauthorised";
            else if (user != null && user.UserName == UserName && user.Password != PassWord) msg.Message = "Invalid Password";
            else if (user != null && user.UserName == UserName && user.Password == PassWord) msg.Message = "Authorized";
            else msg.Message = "Unuthorized";
            long one = 1;
            long zero = 0;
            if (user != null)
            {
                return (user, msg);
            }
            
            return (user, msg);
        }

        public async Task<MessageHelperModel> CreateUserAsync(UserModel user)
        {
            var res = await _repository.CreateUserAsync(user);
            var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
            if (res == 0)
            {
                msg.Message = "Faild To Create";
            }
            else
            {
                msg.Message = "Sucessfully Added";
            }
            return msg;

        }
    }
}
