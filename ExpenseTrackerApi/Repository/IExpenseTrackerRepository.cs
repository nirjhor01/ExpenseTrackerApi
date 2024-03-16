using ExpenseTrackerApi.Helper;
using ExpenseTrackerApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApi.Repository.Interfaces
{
    public interface IExpenseTrackerRepository
    {
        Task<(long, long)> AddSpendingAsync(Expense expense);
        Task<long> GetTotalSum(int Userid, DateTime fromDate, DateTime toDate);
        Task<List<ExpensePercentage>> GetExpensePercentage(int UserId);
        Task<List<Expense>> SearchById(int UserId);
        Task<long> Deposit(Deposit deposit);
        Task<long> UpdateByIdAsync(Expense expense);
        Task<long> DeleteByIdAsync(int Id);
        Task<Expense?> LastExpenseAsync(int UserId);
        Task<long> GetSum(int UserId, string Category, DateTime fromDate, DateTime toDate);
    }
}