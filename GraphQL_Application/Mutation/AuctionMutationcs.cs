using GraphQL_Application.GraphQlModels;
using GraphQL_Application.Models;
using GraphQL_Application.Schema;
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
                auctionType.Bids.Add(new Bid { Amount = bid.Amount, PartyId = bid.PartyId, TimeStamp = DateTime.UtcNow });
                await session.SaveChangesAsync();
                return "Bid Placed";

            }
            return "Bid already exists";

        }

        public async Task<string> PlaceBidFromSql(DataContext context, SqlBidModel bid, string auctionInput)
        {
            var auction = await context.Auctions.Where(i => i.Id == bid.AuctionId).FirstOrDefaultAsync();

            if (auction.StartDate > DateTime.UtcNow || auction.EndDate < DateTime.UtcNow)
            {
                return "auction has not started yet";
            }

            if (!auction.Parties.Any(i=> i.Name == bid.PartyName))
            {
                return "party is not invited to the bid";
            }
            if (context.Bids.Any(i => i.PartyName == bid.PartyName && i.TimeStamp == DateTime.UtcNow))
            {
                return "bid already exists";
            }

            context.Bids.Add(new EFModels.BidT { Amount = bid.Amount, PartyName = bid.PartyName, TimeStamp = DateTime.UtcNow, Auction = auction });
            await context.SaveChangesAsync();
            return "Bid placed!";

        }
    }
}
