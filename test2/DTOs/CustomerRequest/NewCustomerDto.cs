namespace test2.DTOs.CustomerRequest;

public class NewCustomerDto
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string? PhoneNumber { get; set; }
}