using ExpenseTrackerApi.Helper;
using ExpenseTrackerApi.Model;

namespace ExpenseTrackerApi.Logger.Service
{
    public interface ILogService
    {
        Task<MessageHelperModel> CreateLog(Log log);
    }
}
