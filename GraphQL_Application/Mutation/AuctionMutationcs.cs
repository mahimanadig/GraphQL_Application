using GraphQL_Application.GraphQlModels;
using GraphQL_Application.Models;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace GraphQL_Application.Mutation
{
    public class AuctionMutationcs
    {
        public async Task<string> PlaceBid(IAsyncDocumentSession session, BidModel bid, string auction)
        {
            var auctionType = await session.Query<Auction>().Where(i => i.Description == auction).FirstOrDefaultAsync();

            if (!auctionType.Parties.Contains(bid.PartyId))
            {
                return "Party is not invited";
            }
            if (auctionType.EndDate < DateTime.UtcNow && auctionType.StartDate > DateTime.UtcNow)
            {
                return "bid date is invalid";
            }
           
            if (!auctionType.Bids
          .Any(i => i.PartyId == bid.PartyId && i.Amount == bid.Amount))
            {
                auctionType.Bids.Add(new Bid { Amount = bid.Amount, PartyId = bid.PartyId, TimeStamp = DateTime.UtcNow});
                await session.SaveChangesAsync();
                return "Bid Placed";

            }
            return "Bid already exists";

        }
    }
}
