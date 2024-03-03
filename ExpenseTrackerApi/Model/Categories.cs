namespace ExpenseTrackerApi.Model
{
    public class Categories
    {
        public int UserId { get; set; }
        public int Transport {  get; set; }
        public int Food {  get; set; }
        public int EatingOut {  get; set; }  
        public int House {  get; set; }
        public int Cloths { get; set;}
        public int Communication { get; set;}
        public DateTime DateTimeColumn { get; set; }
    }
}
