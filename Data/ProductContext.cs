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

            public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Community> Communities { get; set; } = default!;
    public DbSet<Client> Clients { get; set; } = default!;
    public DbSet<Ticket> Tickets { get; set; } = default!;
    public DbSet<ProductCommunity> ProductCommunity { get; set; } = default!;
    public DbSet<ProductTicket> ProductTicket { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        //INICIA RELACION MUCHOS A MUCHOS EN Product y Community
        modelBuilder.Entity<ProductCommunity>()
            .HasKey(pc => new { pc.ProductId, pc.CommunityId });

        modelBuilder.Entity<ProductCommunity>()
            .HasOne(pc => pc.Product)
            .WithMany(p => p.ProductCommunities)
            .HasForeignKey(pc => pc.ProductId);

        modelBuilder.Entity<ProductCommunity>()
            .HasOne(pc => pc.Community)
            .WithMany(c => c.ProductCommunities)
            .HasForeignKey(pc => pc.CommunityId);
        //TERMINA RELACION MUCHOS A MUCHOS EN Product y Community

        //INICIA RELACION MUCHOS A MUCHOS EN Product y Ticket
        modelBuilder.Entity<ProductTicket>()
            .HasKey(pc => new { pc.ProductId, pc.TicketId });

        modelBuilder.Entity<ProductTicket>()
            .HasOne(pc => pc.Product)
            .WithMany(p => p.ProductTickets)
            .HasForeignKey(pc => pc.ProductId);

        modelBuilder.Entity<ProductTicket>()
            .HasOne(pc => pc.Ticket)
            .WithMany(c => c.ProductTickets)
            .HasForeignKey(pc => pc.TicketId);
        //TERMINA RELACION MUCHOS A MUCHOS EN Product y Ticket

        //INICIA RELACION UNO A MUCHOS EN Ticket y Community
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Community)
            .WithMany(c => c.Tickets)
            .HasForeignKey(t => t.CommunityId);
        //TERMINA RELACION UNO A MUCHOS EN Ticket y Community

        //INICIA RELACION UNO A MUCHOS EN  Ticket  y Client    
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Client)
            .WithMany(c => c.Tickets)
            .HasForeignKey(t => t.ClientId);
        //TERMINA RELACION UNO A MUCHOS EN Ticket  y Client 

    }
    }
}
