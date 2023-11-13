using GraphQL_Application.EFModels;

namespace GraphQL_Application.Queries
{
    public class SqlAuctionQuery
    {
        public IQueryable<AuctionT> GetAuctionsFromSql([Service] DataContext context)
        {
            return context.Auctions.AsQueryable();
        }
    }
}
