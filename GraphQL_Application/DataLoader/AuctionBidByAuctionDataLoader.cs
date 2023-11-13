using GraphQL_Application.EFModels;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace GraphQL_Application.DataLoader
{
    public class AuctionBidByAuctionDataLoader : GroupedDataLoader<int, BidT>
    {
        private readonly DataContext _context;

        public AuctionBidByAuctionDataLoader(DataContext context, IBatchScheduler scheduler, DataLoaderOptions options = null) : base(scheduler, options)
        {
            _context = context;
        }
        protected override async Task<ILookup<int, BidT>> LoadGroupedBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            var bids = await  _context.Bids.Where(i => keys.Contains(i.AuctionId)).ToListAsync();
            return bids.ToLookup(i => i.AuctionId);

        }
    }
}
