namespace Domain.Entities;
public class Property
{
    public int Id { get; set; }
    public int AgentId { get; set; } // FK Agent
    public string ShortDescription { get; set; }
    public string LongDescription { get; set; }
    public decimal Price { get; set; }
    public DateTime ListingDate { get; set; }

    public Agent Agent { get; set; } // Navigation property
}
