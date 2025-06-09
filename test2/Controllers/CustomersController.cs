using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test2.DAL;
using test2.DTOs.CustomerInfo;
using test2.DTOs.CustomerRequest;
using test2.Models;

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
    
    [HttpPost]
    public async Task<IActionResult> AddCustomer(CancellationToken cancellationToken,
        NewCustomerRequestDto request)
    {
        Random random = new Random();
        
        Dictionary<string, int> concertCount = new Dictionary<string, int>();
        
        foreach (var purchase in request.purchases)
        {
            if (concertCount.ContainsKey(purchase.ConcertName))
            {
                concertCount[purchase.ConcertName]++;
            }
            else
            {
                concertCount[purchase.ConcertName] = 1;
            }
            
            var existingConcert = await _dbContext.Concert
                .SingleOrDefaultAsync(c => c.Name == purchase.ConcertName);

            if (existingConcert == null)
            {
                return BadRequest($"There is no concert with name {purchase.ConcertName}");
            }
        }

        foreach (var concert in concertCount)
        {
            if (concert.Value > 5)
            {
                return BadRequest("Can not buy more than 5 tickets for a single concert.");
            }
        }
        
        var existingCustomer = await _dbContext.Customer
            .SingleOrDefaultAsync(c => c.CustomerId == request.customer.Id, cancellationToken);
        
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        
        if (existingCustomer == null)
        {
            var newCustomer = new Customer
            {
                CustomerId = request.customer.Id,
                FirstName = request.customer.FirstName,
                LastName = request.customer.LastName,
                PhoneNumber = request.customer.PhoneNumber
            };
            await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Customer ON", cancellationToken);
            await _dbContext.Customer.AddAsync(newCustomer, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Customer ON", cancellationToken);

        }
        else
        {
            return BadRequest(
                $"Customer with Id {request.customer.Id} already exists");
        }
        

        foreach (var purchase in request.purchases)
        {
            await _dbContext.Ticket.AddAsync(new Ticket
            {
                SerialNumber = $"TK{random.Next(1000, 9999)}/S{random.Next(1000, 9999)}/{random.Next(10, 99)}",
                SeatNumber = purchase.SeatNumber
            }, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var newTicket = await _dbContext.Ticket
                .OrderByDescending(t => t.TicketId).FirstOrDefaultAsync(cancellationToken);

            var concert = await _dbContext.Concert
                .FirstOrDefaultAsync(c => c.Name == purchase.ConcertName, cancellationToken);
            
            
            await _dbContext.TicketConcert.AddAsync(new TicketConcert
            {
                TicketId = newTicket.TicketId,
                ConcertId = concert.ConcertId,
                Price = purchase.Price
            }, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var newTicketConcert = await _dbContext.TicketConcert
                .OrderByDescending(tc => tc.TicketConcertId).FirstOrDefaultAsync(cancellationToken);

            
            await _dbContext.PurchasedTicket.AddAsync(new PurchasedTicket
            {
                TicketConcertId = newTicketConcert.TicketConcertId,
                CustomerId = request.customer.Id,
                PurchaseDate = DateTime.Today
            }, cancellationToken);
        }
        
        await transaction.CommitAsync(cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Ok();
    }
    
}