using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FinalHPMS.Models;
using FinalHPMS.Migrations;

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
        public DbSet<FinalHPMS.Models.Ticket> Ticket {get;set;} =default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  modelBuilder.Entity<Community>()
            // .HasMany(community => community.Products) // Una comunidad tiene muchos productos
            // .WithOne(product => product.Community)    // Un producto pertenece a una comunidad
            // .HasForeignKey(product => product.CommunityId);

            modelBuilder.Entity<Product>()
            .HasMany(product => product.Communities)
            .WithMany(community => community.Products)
            .UsingEntity("ProductCommunity");
        
            modelBuilder.Entity<Client>()
            .HasMany(c=>c.Tickets)
            .WithOne(c=>c.Client)
            .HasForeignKey(c=>c.ClientId);

            modelBuilder.Entity<Community>()
            .HasMany(c=>c.Tickets)
            .WithOne(c=>c.Community)
            .HasForeignKey(c=>c.CommunityId);

            //base.OnModelCreating(modelBuilder);
            
            // modelBuilder.Entity<Ticket>()
            // .HasOne(c=>c.Client)
            // .WithMany(c=>c.Tickets)
            // .HasForeignKey(c=>c.ClientId);
        
        }
    }
}
