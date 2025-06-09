namespace test2.DTOs.CustomerInfo;

public class CustomerInfoDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public List<CustomerInfoPurchaseDto> Purchases { get; set; }
}