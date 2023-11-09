namespace GraphQL_Application.Models
{
    public class Bid
    {
        public DateTime TimeStamp { get; set; }
        public int  PartyId { get; set; }
        public decimal Amount { get; set; }
    }
}
