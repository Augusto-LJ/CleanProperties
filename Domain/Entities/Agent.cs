namespace Domain.Entities;
public class Agent
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public List<Property> PropertyListings { get; set; } // Navigation property
}
