namespace GraphQL_Application.Models
{
    public class Auction
    {
        public string Description { get; set; } = string.Empty;
     
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
     
        public DateTime EndDate { get; set; } = DateTime.UtcNow;

        public List<int>? Parties { get; set; }

        public List<Bid>? Bids { get; set; }
    }
}
