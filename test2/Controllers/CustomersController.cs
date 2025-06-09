using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test2.DAL;
using test2.DTOs.CustomerInfo;

namespace test2.Controllers;

[ApiController]
[Route($"api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ConcertsDbContext _dbContext;

    public CustomersController(ConcertsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{id:int}/purchases")]
    public async Task<IActionResult> GetCustomer(CancellationToken cancellation, int id)
    {
        var result = await _dbContext
            .Customer
            .Where(c => c.CustomerId == id)
            .Select(c => new CustomerInfoDto
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
                Purchases = c.purchases.OrderBy(pt => pt.PurchaseDate)
                    .Select(pt => new CustomerInfoPurchaseDto
                    {
                        Date = pt.PurchaseDate,
                        Price = pt.TicketConcert.Price,
                        Ticket = new CustomerInfoTicketDto
                        {
                            Serial = pt.TicketConcert.Ticket.SerialNumber,
                            SeatNumber = pt.TicketConcert.Ticket.SeatNumber
                        },
                        Concert = new CustomerInfoConcertDto
                        {
                            Name = pt.TicketConcert.Concert.Name,
                            Date = pt.TicketConcert.Concert.Date
                        }
                    }).ToList()
            }).SingleOrDefaultAsync(cancellation);
        
        if (result == null)
        {
            return NotFound($"Customer with Id {id} was not found.");
        }
        
        return Ok(result);
    }
    
}