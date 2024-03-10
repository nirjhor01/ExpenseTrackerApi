namespace ExpenseTrackerApi.Model
{
    public class Expense
    {
        public int UserId { get; set; }
        public string Category { get; set; }
        public int Amount { get; set; }
        public DateTime DateTimeInfo { get; set; }

    }
}
