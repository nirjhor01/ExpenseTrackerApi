using ExpenseTrackerApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ExpenseTrackerApi.Service;
using ExpenseTrackerApi.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.OpenApi.Validations;

namespace ExpenseTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IExpeneseTrackerServices _services;
        public UserController(IExpeneseTrackerServices services)
        {
            _services = services;
        }

       /* [HttpGet("Admins")]
        [Authorize]
        public string AdminsEndPoint()
        {
           // var currentUser = GetCurrentUser();
           // return Ok($"HI {currentUser.UserName},you are an{currentUser.Role}");
           return "401";
        }*/
        [HttpPost]
        [Route("AddSpending")]
        public async Task<IActionResult> AddSpending( Categories categories)
        {
            var res = await _services.AddSpendingAsync(categories);
            return Ok(res);

        }
        [HttpGet]
        [Route("GetTransportSum")]
        public async Task<IActionResult> TransportSum(DateTime fromDate, DateTime toDate)
        {
            var res = await _services.GetTransportSum(fromDate, toDate);
            return Ok(res);
        }
        [HttpGet]
        [Route("GetTotalSum")]
        public async Task<IActionResult> TotalSum(int UserId, DateTime fromDate, DateTime toDate)
        {
            var res = await _services.GetTotalSum(UserId, fromDate, toDate);
            return Ok(res);
        }
        [HttpGet]
        [Route("ExpensePercentage")]
        public async Task<IActionResult> ExpensePercentage(int UserId)
        {
            var res = await _services.GetExpensePercentage(UserId);
            return Ok(new { info = res.Item1, msg = res.Item2 }) ;
        }














    }
}
