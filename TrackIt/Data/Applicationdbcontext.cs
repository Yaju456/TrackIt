using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TrackIt.Models;

namespace TrackIt.Data
{
    public class Applicationdbcontext : IdentityDbContext
    {
        public Applicationdbcontext(DbContextOptions<Applicationdbcontext>options): base(options)
        {
            
        }

        public DbSet<ProductClass> ProductTable { get; set; }
        
        public DbSet<VendorClass> VendorTable { get; set; }
        public DbSet<ClinetClass> ClientTable { get; set;}
        public DbSet<OrderClass> OrderTable { get; set; }

        public DbSet<SalesClass> SalesTable { get; set; }

        public DbSet<StockClass> StockTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StockClass>().HasAlternateKey(a => a.serial_number);
        }
    }
}
