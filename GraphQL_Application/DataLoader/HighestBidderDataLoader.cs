using GraphQL_Application.Models;
using Microsoft.AspNetCore.Http;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System.Linq;

namespace GraphQL_Application.DataLoader
{
    public class HighestBidderDataLoader /*: BatchDataLoader<List<Bid>, string>*/
    {
        //private readonly IAsyncDocumentSession _session;

        //public HighestBidderDataLoader(IAsyncDocumentSession session, IBatchScheduler scheduler, DataLoaderOptions options = null) : base(scheduler, options)
        //{
        //    _session = session;
        //}
        //protected override async Task<IReadOnlyDictionary<List<Bid>, string>> LoadBatchAsync(IReadOnlyList<List<Bid>> keys, CancellationToken cancellationToken)
        //{
        //    var highBid = await _session.Query<Auction>()
        //        .Where(i => i.Description == "").FirstOrDefaultAsync();

        //    var partyId = highBid.Bids.OrderByDescending(i => i.Amount).Select(i=>i.PartyId).FirstOrDefault();

        //    var party = await _session.Query<Party>().Where(i=>i.PartyId == partyId).FirstOrDefaultAsync();

        //    return party.Name;
            
        //    //    .Select(i => i.Bids.Max(i => i.Amount)).FirstOrDefaultAsync();
        //    //hightBid.
        //    //return await _session.Query<Party>().Where(i => keys.Contains(i.PartyId)).Select(i => i.Name);
        //}

    }
}
