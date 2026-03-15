namespace Application.Models.Responses;
public class AgentResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public List<PropertyResponse> PropertyListings { get; set; } 
}
