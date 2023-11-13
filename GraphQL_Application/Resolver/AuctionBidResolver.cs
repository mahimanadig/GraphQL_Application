using GraphQL_Application.DataLoader;
using GraphQL_Application.EFModels;

namespace GraphQL_Application.Resolver
{
    public class AuctionBidResolver
    {
        public async Task<List<BidT>> GetAuctionBidsasync(AuctionT auction, AuctionBidByAuctionDataLoader dataLoader)
        {
            var bids = await dataLoader.LoadAsync(auction.Id);
            return bids.ToList();
        }
    }
}
