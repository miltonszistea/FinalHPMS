using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FinalHPMS.Models;

namespace FinalHPMS.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext (DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public DbSet<FinalHPMS.Models.Product> Product { get; set; } = default!;
        public DbSet<FinalHPMS.Models.Community> Community {get;set;} =default!;
        public DbSet<FinalHPMS.Models.Client> Client {get;set;} =default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Product>()
            .HasMany(c=>c.Communities)
            .WithMany(p=>p.Products)
            .UsingEntity("ProductCommunity");
        
            modelBuilder.Entity<Client>()
            .HasMany(c=>c.Tickets)
            .WithOne(c=>c.Client)
            .HasForeignKey(c=>c.ClientId);

            modelBuilder.Entity<Community>()
            .HasMany(c=>c.Tickets)
            .WithOne(c=>c.Community)
            .HasForeignKey(c=>c.CommunityId);

            // modelBuilder.Entity<Ticket>()
            // .HasOne(c=>c.Client)
            // .WithMany(c=>c.Tickets)
            // .HasForeignKey(c=>c.ClientId);
        
        }
    }
}
