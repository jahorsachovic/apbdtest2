using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace test2.Models;


public class TicketConcert
{
    [Key]
    public int TicketConcertId { get; set; }
    
    public int TicketId { get; set; }
    
    public int ConcertId { get; set; }

    [Precision(10)]
    public decimal Price { get; set; }
    
    [ForeignKey(nameof(TicketId))]
    public Ticket Ticket { get; set; }
    [ForeignKey(nameof(ConcertId))]
    public Concert Concert { get; set; }   
}