namespace NailSalon.Models
{
    public class Booking
    {

        public int Id { get; set; }
        public string StaffName { get; set; }
        public DateTime Time { get; set; }
        public string CustomerName { get; set; }
        public string ServiceName { get; set; }

    }
}
