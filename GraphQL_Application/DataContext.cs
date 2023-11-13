using GraphQL_Application.EFModels;
using Microsoft.EntityFrameworkCore;

namespace GraphQL_Application
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DataContext()
        { }

        public DbSet<AuctionT> Auctions { get; set; }
        public DbSet<PartyT> Parties { get; set; }
        public DbSet<BidT> Bids { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<AuctionT>()
            //  .Property(f => f.Id)
            //  .ValueGeneratedOnAdd();

            //modelBuilder.Entity<PartyT>()
            //    .Property(f => f.Id)
            //    .ValueGeneratedOnAdd();

            //modelBuilder.Entity<BidT>()
            //    .Property(f => f.Id)
            //    .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
        }
    }
}
