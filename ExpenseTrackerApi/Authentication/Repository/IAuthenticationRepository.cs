using ExpenseTrackerApi.Helper;
using ExpenseTrackerApi.Model;

namespace ExpenseTrackerApi.Authentication.Repository
{
    public interface IAuthenticationRepository
    {
        Task<UserLogin?> UserLogInAsync(string UserName, string PassWord);
        Task<long> CreateUserAsync(UserModel user);
    }
}
