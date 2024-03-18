using ExpenseTrackerApi.Controllers;
using ExpenseTrackerApi.Helper;
using ExpenseTrackerApi.Logger.Service;
using ExpenseTrackerApi.Model;
using ExpenseTrackerApi.Logger;
using ExpenseTrackerApi.Repository.Interfaces;
using ExpenseTrackerApi.Service.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerApi.Logger.Repository;

namespace ExpenseTrackerApi.Service.Implementations
{
    public class ExpenseTrackerServices : IExpeneseTrackerServices
    {

        private readonly IExpenseTrackerRepository _repository;
        private readonly ILogService _logService;

        public ExpenseTrackerServices(IExpenseTrackerRepository repository, ILogService logService)
        {
            _repository = repository;
            _logService = logService;
        }

        public async Task<MessageHelperModel> AddSpendingAsync(Expense expense)
        {
            var res = await _repository.AddSpendingAsync(expense);
            MessageHelperModel msg = new MessageHelperModel();
            if (res.Item2 <= 0)
            {
                Log log = new Log
                {
                    LogInformation = "Expenditure exceeds deposit amount",
                    TimeStamp = DateTime.Now
                };
                var logMsg = await _logService.CreateLog(log);

                msg.Message = "Expenditure exceeds deposit amount";
                msg.StatusCode = 200;
            }
            else
            {
                Log log = new Log
                {
                    LogInformation = "Sucessfully Added Spending",
                    TimeStamp = DateTime.Now
                };
                var logMsg = await _logService.CreateLog(log);
                msg.Message = "Sucessfully Spending";
            }
            return msg;


        }





        public async Task<(List<ExpensePercentage>, MessageHelperModel)> GetExpensePercentage(int UserId)
        {

            var res = await _repository.GetExpensePercentage(UserId);
            var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
            if (res == null)
            {
                Log log = new Log
                {
                    LogInformation = "Faild to get percentage",
                    TimeStamp = DateTime.Now
                };
                var logMsg = await _logService.CreateLog(log);
                msg.Message = "Faild to get percentage";
            }
            else
            {
                Log log = new Log
                {
                    LogInformation = "Sucessfully get percentage",
                    TimeStamp = DateTime.Now
                };
                var logMsg = await _logService.CreateLog(log);
                msg.Message = "Sucessfully get percentage";
            }
            return (res, msg);

        }

        public async Task<long> GetTotalSum(int Userid, DateTime fromdate, DateTime toDate)
        {
            var res = await _repository.GetTotalSum(Userid, fromdate, toDate);
            return res;

        }

        public async Task<(MessageHelperModel, List<Expense>)> SearchById(int UserId)
        {
            var res = await _repository.SearchById(UserId);
            var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
            if (res == null || res.Count == 0)
            {
                Log log = new Log
                {
                    LogInformation = "Failed to get data by user",
                    TimeStamp = DateTime.Now
                };
                var logMsg = await _logService.CreateLog(log);
                msg.Message = "Failed to get data";
            }
            else
            {
                Log log = new Log
                {
                    LogInformation = "Successfully got the data",
                    TimeStamp = DateTime.Now
                };
                var logMsg = await _logService.CreateLog(log);
                msg.Message = "Successfully got the data";
            }
            return (msg, res);
        }





        public async Task<MessageHelperModel> Deposit(Deposit deposit)
        {
            var res = await _repository.Deposit(deposit);
            var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
            if (res == 0)
            {
                Log log = new Log
                {
                    LogInformation = "Failed to Deposit",
                    TimeStamp = DateTime.Now
                };
                var logMsg = await _logService.CreateLog(log);
                msg.Message = "Failed to Deposit";
               
            }
            else
            {
                Log log = new Log
                {
                    LogInformation = "Sucessfully Deposited",
                    TimeStamp = DateTime.Now
                };
                var logMsg = await _logService.CreateLog(log);
                msg.Message = "Sucessfully Deposited";
            }
            return msg;

        }
        public async Task<MessageHelperModel> UpdateByIdAsync(Expense expense)
        {
            var res = await _repository.UpdateByIdAsync(expense);
            var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
            if (res == 0)
            {
                Log log = new Log
                {
                    LogInformation = "User Update data",
                    TimeStamp = DateTime.Now
                };
                var logMsg = await _logService.CreateLog(log);
                msg.Message = "Failed To Update";
            }
            else
            {
                Log log = new Log
                {
                    LogInformation = "Sucessfully Updated",
                    TimeStamp = DateTime.Now
                };
                var logMsg = await _logService.CreateLog(log);
       
                msg.Message = "Sucessfully Updated";
            }
            return msg;

        }

        public async Task<MessageHelperModel> DeleteByIdAsync(int Id)
        {
            var res = await _repository.DeleteByIdAsync(Id);
            var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
            if (res == 0)
            {
                Log log = new Log
                {
                    LogInformation = "Failed To Delete Row",
                    TimeStamp = DateTime.Now
                };
                var logMsg = await _logService.CreateLog(log);
                msg.Message = "Failed To Delete Row";
            }
            else
            {
                Log log = new Log
                {
                    LogInformation = "Sucessfully Deleted",
                    TimeStamp = DateTime.Now
                };
                var logMsg = await _logService.CreateLog(log);
                msg.Message = "Sucessfully Deleted";
            }
            return msg;

        }

        public async Task<(MessageHelperModel, long)> GetSum(int UserId, string Category, DateTime fromDate, DateTime toDate)
        {
            var res = await _repository.GetSum(UserId, Category, fromDate, toDate);
            var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
            if (res == 0)
            {
                Log log = new Log
                {
                    LogInformation = "Failed To Retrive Information",
                    TimeStamp = DateTime.Now
                };
                var logMsg = await _logService.CreateLog(log);
                msg.Message = "Failed To Retrive Information";
            }
            else
            {
                Log log = new Log
                {
                    LogInformation = "Sucessfully Retrived",
                    TimeStamp = DateTime.Now
                };
                var logMsg = await _logService.CreateLog(log);

                msg.Message = "Sucessfully Retrived";
            }
            return (msg, res);

        }



        public async Task<Expense> LastExpenseAsync(int UserId)
        {
            var res = await _repository.LastExpenseAsync(UserId);
            var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
            if (res == null)
            {
                Log log = new Log
                {
                    LogInformation = "Failed To Delete Row",
                    TimeStamp = DateTime.Now
                };
                var logMsg = await _logService.CreateLog(log);
                msg.Message = "Failed To Delete Row";
            }
            else
            {
                Log log = new Log
                {
                    LogInformation = "Sucessfully Deleted",
                    TimeStamp = DateTime.Now
                };
                var logMsg = await _logService.CreateLog(log);
                msg.Message = "Sucessfully Deleted";
            }
            return res;

        }







    }
}
