﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TrackIt.Models;

namespace TrackIt.Data
{
    public class Applicationdbcontext : IdentityDbContext
    {
        private readonly IConfiguration _configuration;
        public Applicationdbcontext(DbContextOptions<Applicationdbcontext>options, IConfiguration configuration): base(options)
        {
            _configuration=configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("connect_db"));
        }
        public DbSet<ProductClass> ProductTable { get; set; }
        
        public DbSet<VendorClass> VendorTable { get; set; }
        public DbSet<ClinetClass> ClientTable { get; set;}
        public DbSet<OrderClass> OrderTable { get; set; }

        public DbSet<SalesClass> SalesTable { get; set; }

        public DbSet<StockClass> StockTable { get; set; }

        public DbSet<CustomerClass> CustomerTable { get; set; }

        public DbSet<ProvinceClass> Province { get; set; }
        public DbSet<DistrictClass> District { get; set; }
        public DbSet<LocalBodyClass> LocalBody { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StockClass>().HasIndex(a => a.serial_number).IsUnique();
        }
    }
}
