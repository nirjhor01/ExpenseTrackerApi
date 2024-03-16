

using ExpenseTrackerApi.Helper;
using ExpenseTrackerApi.Logger.Repository;
using ExpenseTrackerApi.Model;

namespace ExpenseTrackerApi.Logger.Service
{
    public class LogService:ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly IConfiguration _configuration;
        public LogService(IConfiguration configuration, ILogRepository logrepository)
        {
            _logRepository = logrepository;
            _configuration = configuration;
        }
        public async Task<MessageHelperModel> CreateLog(Log log)
        {
            try
            {
                var res = await _logRepository.CreateLog(log);
                var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
                if(res != 0)
                {
                    msg.Message = "Log Successfully Created";
                }
                else
                {
                    msg.Message = "Unsuccessful to create Log";
                }
                return msg;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
