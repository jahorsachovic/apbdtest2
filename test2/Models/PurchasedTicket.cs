using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace test2.Models;

[PrimaryKey(nameof(TicketConcertId), nameof(CustomerId))]
public class PurchasedTicket
{
    public int TicketConcertId { get; set; }
    
    public int CustomerId { get; set; }
    
    public DateTime PurchaseDate { get; set; }

    [ForeignKey(nameof(TicketConcertId))]
    public TicketConcert TicketConcert { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; }
    
}