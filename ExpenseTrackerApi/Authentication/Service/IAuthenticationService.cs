using ExpenseTrackerApi.Helper;
using ExpenseTrackerApi.Model;

namespace ExpenseTrackerApi.Service.Interfaces
{
    public interface IAuthenticationService 
    {
        Task<(UserLogin, MessageHelperModel)> UserLogInAsync(string UserName, string PassWord);
        string GenerateToken(string UserName);
        Task<MessageHelperModel> CreateUserAsync(UserModel user);
    }
}
