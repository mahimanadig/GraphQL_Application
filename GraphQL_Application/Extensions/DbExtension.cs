using GraphQL_Application.EFModels;
using GraphQL_Application.Models;
using Raven.Client.Documents;
using Sparrow.Binary;

namespace GraphQL_Application.Extensions
{
    public static class DbExtension
    {
        public static WebApplication SeedSQLData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            using var context = scope.ServiceProvider.GetService<DataContext>();

            if (!context.Database.CanConnect())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            context.Auctions.AddRange(new[]
            {
                new AuctionT {Id = 1, Description = "Auction1",
                    Bids = new List<BidT>
                    {
                         new BidT {Amount=100,PartyName="Mahima",TimeStamp = DateTime.UtcNow},
                         new BidT {Amount=200,PartyName="Safan",TimeStamp = DateTime.UtcNow}
                    },
                    Parties = new List<PartyT>
                    {
                        new PartyT { Name ="Mahima"},
                        new PartyT { Name ="Safan"},
                        new PartyT { Name ="Niranjan"},
                    },
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(1)},
                new AuctionT {Id = 2,
                    Description = "Auction2",
                    Parties = new List<PartyT>
                    {
                        new PartyT { Name ="Safan"},
                        new PartyT { Name ="Niranjan"},
                    },
                    StartDate = DateTime.UtcNow.AddDays(1),
                    EndDate = DateTime.UtcNow.AddDays(2)},
                new AuctionT {Id = 3,
                    Description = "Auction3",
                    Bids  = new List<BidT>
                    {
                         new BidT {Amount=100,PartyName="Niranjan",TimeStamp = DateTime.UtcNow},
                         new BidT {Amount=200,PartyName="Safan",TimeStamp = DateTime.UtcNow}
                    },
                     Parties = new List<PartyT>
                    {
                        new PartyT { Name ="Mahima"},
                        new PartyT { Name ="Safan"},
                    },
                    StartDate = DateTime.UtcNow.AddDays(-2),
                    EndDate = DateTime.UtcNow.AddDays(-1)},
            });

            context.SaveChanges();

            return app;
        }
        public static WebApplication SeedData(this WebApplication app)
        {
            var parties = new List<Party>
            {
                new Party{ PartyId = 1, Name = "Mahima"},
                new Party{ PartyId = 2, Name = "Safan"},
                new Party{ PartyId = 3, Name = "Niranjan"},
            };

            var seed = new List<Auction>
            {
               new Auction
               {
                   Description = "Auction1",
                   StartDate = DateTime.UtcNow,
                   EndDate = DateTime.UtcNow.AddDays(1),
                   Bids = new List<Bid>
                   {
                       new Bid { PartyId = 1, Amount = 100, TimeStamp = DateTime.UtcNow },
                       new Bid { PartyId = 1, Amount = 200, TimeStamp = DateTime.UtcNow.AddHours(-2) },
                       new Bid { PartyId = 2, Amount = 500, TimeStamp = DateTime.UtcNow }
                   },
                   Parties = new List<int> {1,2 }
               },
                new Auction
               {
                   Description = "Auction2",
                   StartDate = DateTime.UtcNow.AddDays(2),
                   EndDate = DateTime.UtcNow.AddDays(3),
                   Parties = new List<int> {2,3 }
               },
                 new Auction
               {
                   Description = "Auction3",
                   StartDate = DateTime.UtcNow.AddDays(-2),
                   EndDate = DateTime.UtcNow.AddDays(-1),
                   Bids = new List<Bid>
                   {
                       new Bid { PartyId = 3, Amount = 100, TimeStamp = DateTime.UtcNow.AddDays(-2) },
                       new Bid { PartyId = 3, Amount = 200, TimeStamp = DateTime.UtcNow.AddDays(-2) },
                       new Bid { PartyId = 2, Amount = 500, TimeStamp = DateTime.UtcNow.AddDays(-1) }
                   },
                   Parties = new List<int> {1,2,3 }
               },
           };

            using var scope = app.Services.CreateScope();
            using var session = scope.ServiceProvider
                .GetService<IDocumentStore>()
                .OpenSession();

            if (!session.Query<Party>().Any())
            {
                foreach (var party in parties)
                {
                    session.Store(party);
                }

                session.SaveChanges();
            }
            if (!session.Query<Auction>().Any())
            {
                foreach (var auction in seed)
                {
                    session.Store(auction);
                }

                session.SaveChanges();
            }
            return app;
        }
    }
}
