
using ExpenseTrackerApi.Helper;
using ExpenseTrackerApi.Model;

namespace ExpenseTrackerApi.Logger.Repository
{
    public interface ILogRepository
    {
        Task<long> CreateLog(Log log);
    }
}
