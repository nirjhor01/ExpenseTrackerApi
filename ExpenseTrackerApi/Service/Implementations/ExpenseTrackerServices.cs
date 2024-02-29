using ExpenseTrackerApi.Controllers;
using ExpenseTrackerApi.Helper;
using ExpenseTrackerApi.Model;
using ExpenseTrackerApi.Repository.Interfaces;
using ExpenseTrackerApi.Service.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ExpenseTrackerApi.Service.Implementations
{
    public class ExpenseTrackerServices : IExpeneseTrackerServices
    {
      
        private readonly IExpenseTrackerRepository _repository;
        public ExpenseTrackerServices(IExpenseTrackerRepository repository)
        {
            _repository = repository;
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


        public async Task<(UserLogin, MessageHelperModel)> LoginUserAsync(string UserName, string PassWord)
        {
  

          
            var user = await _repository.UserLogInAsync(UserName, PassWord);
           

            var msg = new MessageHelperModel { Message = "", StatusCode = 200 };

            if (user == null)
            {
                msg.Message = "Invalid UserName";
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
            }
            return (user, msg);
        }




    }
}
