using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerApi.Model
{
    public class Expense
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Category { get; set; }
        public int Amount { get; set; }
        public DateTime DateTimeInfo { get; set; }
        public string? Note { get; set; }


    }
}
