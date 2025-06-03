namespace Presentation.Models
{
    public class EventDto
    {
        public string EventName { get; set; } = null!;
        public string? EventDescription { get; set; }
        public string? EventImage { get; set; }
        public string Location { get; set; } = null!;
        public string City { get; set; } = null!;
        public string? State { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public DateTime TicketStartDate { get; set; }
        public int TicketAmount { get; set; }
        public decimal Price { get; set; }
    }
}
