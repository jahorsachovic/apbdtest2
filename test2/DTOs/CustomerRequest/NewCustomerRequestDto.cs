namespace test2.DTOs.CustomerRequest;

public class NewCustomerRequestDto
{
    public NewCustomerDto customer { get; set; }
    public List<NewPurchaseDto> purchases { get; set; }
}