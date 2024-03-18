using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerApi.Model
{
    public class Expense
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string? Category { get; set; }
        [Required]
        public int Amount { get; set; }
        public DateTime DateTimeInfo { get; set; }
        public string? Note { get; set; }


    }
}
