using GraphQL_Application.EFModels;
using GraphQL_Application.Resolver;

namespace GraphQL_Application.Schema
{
    public class SqlAuctionType:ObjectType<AuctionT>
    {
        protected override void Configure(IObjectTypeDescriptor<AuctionT> descriptor)
        {

            descriptor.Field(x => x.Bids).ResolveWith<AuctionBidResolver>(x => x.GetAuctionBidsasync(default!, default!));

            base.Configure(descriptor);
        }
    }
}
