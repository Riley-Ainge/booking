namespace Assignment2.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> Start { get; set; }
        public Nullable<System.DateTime> End { get; set; }
        public Nullable<int> CentreID { get; set; }
    }
}
