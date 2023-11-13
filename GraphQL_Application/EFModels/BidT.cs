using System.ComponentModel.DataAnnotations;

namespace GraphQL_Application.EFModels
{
    public class BidT
    {
        [Key]
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PartyName { get; set; }
        public decimal Amount { get; set; }
        public int AuctionId {  get; set; }

        public virtual AuctionT Auction { get; set; }
    }
}
