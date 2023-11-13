using GraphQL_Application.Models;
using System.ComponentModel.DataAnnotations;

namespace GraphQL_Application.EFModels
{
    public class AuctionT
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        public DateTime EndDate { get; set; } = DateTime.UtcNow;

        public virtual ICollection<PartyT>? Parties { get; set; }

        public virtual ICollection<BidT>? Bids { get; set; }
    }
}
