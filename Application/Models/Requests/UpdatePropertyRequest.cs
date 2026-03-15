namespace Application.Models.Requests;
public class UpdatePropertyRequest
{
    public int Id { get; set; }
    public int AgentId { get; set; }
    public string ShortDescription { get; set; }
    public string LongDescription { get; set; }
    public decimal Price { get; set; }
}
