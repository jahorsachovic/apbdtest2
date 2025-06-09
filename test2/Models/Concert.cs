using System.ComponentModel.DataAnnotations;

namespace test2.Models;

public class Concert
{
    [Key]
    public int ConcertId { get; set; }

    [MaxLength(100)]
    public string Name { get; set; }

    public DateTime Date { get; set; }

    public int AvailableTickets { get; set; }
}