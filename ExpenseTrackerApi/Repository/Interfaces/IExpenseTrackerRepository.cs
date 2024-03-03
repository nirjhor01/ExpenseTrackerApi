using ExpenseTrackerApi.Helper;
using ExpenseTrackerApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApi.Repository.Interfaces
{
    public interface IExpenseTrackerRepository
    {
        Task<long> CreateUserAsync(UserModel user);
        Task<UserLogin?> UserLogInAsync(string UserName, string PassWord);
        Task<long> AddSpendingAsync(Categories categories);
        Task<long> GetTransportSum(DateTime fromDate, DateTime toDate);

    }
}
