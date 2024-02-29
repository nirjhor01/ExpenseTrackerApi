using ExpenseTrackerApi.Helper;
using ExpenseTrackerApi.Model;

namespace ExpenseTrackerApi.Repository.Interfaces
{
    public interface IExpenseTrackerRepository
    {
        Task<long> CreateUserAsync(UserModel user);
        Task<UserLogin?> UserLogInAsync(string UserName, string PassWord);

    }
}
