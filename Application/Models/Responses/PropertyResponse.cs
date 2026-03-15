namespace Application.Models.Responses;
public class PropertyResponse
{
    public int Id { get; set; }
    public int AgentId { get; set; }
    public string ShortDescription { get; set; }
    public string LongDescription { get; set; }
    public decimal Price { get; set; }
    public AgentResponse Agent { get; set; }
}
