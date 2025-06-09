namespace test2.DTOs.CustomerInfo;

public class CustomerInfoPurchaseDto
{
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
    public CustomerInfoTicketDto Ticket { get; set; }
    public CustomerInfoConcertDto Concert { get; set; }
}