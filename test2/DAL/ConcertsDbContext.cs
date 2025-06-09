using Microsoft.EntityFrameworkCore;
using test2.Models;

namespace test2.DAL;

public class ConcertsDbContext : DbContext
{
    public DbSet<Ticket> Ticket { get; set; }
    public DbSet<Concert> Concert { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<TicketConcert> TicketConcert { get; set; }
    public DbSet<PurchasedTicket> PurchasedTicket { get; set; }
    
    protected ConcertsDbContext()
    {
    }

    public ConcertsDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Ticket>().HasData(
            new Ticket
            {
                TicketId = 1,
                SerialNumber = "TK2034/S4531/12",
                SeatNumber = 124
            },
            new Ticket
            {
                TicketId = 2,
                SerialNumber = "TK2027/S4831/133",
                SeatNumber = 330
            });
        
        modelBuilder.Entity<Concert>().HasData(
            new Concert
            {
                ConcertId = 1,
                Name = "Concert 1",
                Date = new DateTime(2025, 6, 7, 9, 0, 0)
            },
            new Concert
            {
                ConcertId = 14,
                Name = "Concert 14",
                Date = new DateTime(2025, 6, 10, 9, 0, 0)
            });

        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                CustomerId = 1,
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = null
            });
        
        modelBuilder.Entity<TicketConcert>().HasData(
            new TicketConcert
            {
                TicketConcertId = 1,
                TicketId = 1,
                ConcertId = 1,
                Price = new decimal(33.4)
            },
            new TicketConcert
            {
                TicketConcertId = 2,
                TicketId = 2,
                ConcertId = 14,
                Price = new decimal(48.4)
            });
        
        modelBuilder.Entity<PurchasedTicket>().HasData(
            new PurchasedTicket
            {
                TicketConcertId = 1,
                CustomerId = 1,
                PurchaseDate = new DateTime(2025, 6, 3, 9, 0, 0)
            },
            new PurchasedTicket
            {
                TicketConcertId = 2,
                CustomerId = 1,
                PurchaseDate = new DateTime(2025, 6, 3, 9, 0, 0)
            });
    }
}