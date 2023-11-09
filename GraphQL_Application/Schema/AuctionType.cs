using GraphQL_Application.DataLoader;
using GraphQL_Application.Models;
using HotChocolate.Resolvers;

namespace GraphQL_Application.Schema
{
    public class AuctionType:ObjectType<Auction>
    {
        protected override void Configure(IObjectTypeDescriptor<Auction> descriptor)
        {
            descriptor.Field("HighBidder").Resolve(HighestBidder);
            base.Configure(descriptor);
        }

        //private string HighestBidder(IResolverContext context)
        //{
        //    //loop thrugh the list of bids and fetch the highest amound and the partyId accordingly.
        //    //fetch the party name by party id
        //    var partyId = context.DataLoader<HighestBidderDataLoader>();
        //    var parent = context.Parent<Auction>();
        //    //return  parent.Bids.OrderByDescending(i => i.Amount).Select(i => i.PartyId).FirstOrDefault();

        //    return partyId.LoadAsync(parent.Bids);
        //}

        private int HighestBidder(IResolverContext context)
        {
            //loop thrugh the list of bids and fetch the highest amound and the partyId accordingly.
            //fetch the party name by party id
            var parent = context.Parent<Auction>();
            return parent.Bids.OrderByDescending(i => i.Amount).Select(i => i.PartyId).FirstOrDefault();

        }
    }
}
