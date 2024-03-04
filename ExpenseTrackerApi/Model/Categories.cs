namespace ExpenseTrackerApi.Model
{
    public class Categories
    {
        public int UserId { get; set; }
        public int TransportSum {  get; set; }
        public int FoodSum {  get; set; }
        public int EatingOutSum {  get; set; }  
        public int HouseSum {  get; set; }
        public int ClothsSum { get; set;}
        public int CommunicationSum { get; set;}
        public int TotalSum { get; set; }
        public DateTime DateTimeColumn { get; set; }
    }
}
