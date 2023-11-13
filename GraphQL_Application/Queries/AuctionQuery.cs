using GraphQL_Application.EFModels;
using GraphQL_Application.Models;
using HotChocolate.Data;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;

namespace GraphQL_Application.Queries
{
    //[ExtendObjectType("Query")]
    public class AuctionQuery
    {
        [UseFiltering]
        public async Task<IRavenQueryable<Auction>> GetAuctions(IAsyncDocumentSession session, string partyName)
        {
            var partyId = session.Query<Party>().Where(i => i.Name.Equals(partyName)).Select(i => i.PartyId).FirstOrDefaultAsync();
            return session.Query<Auction>().Where(i => i.Parties != null && i.Parties.Contains(partyId.Result));
        }

       
    }
}
