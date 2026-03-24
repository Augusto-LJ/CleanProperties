namespace Application;
public class CacheSettings
{
    public int SlidingExpirationInMinutes { get; set; }
    public string DestinationUrl { get; set; }
    public string ApplicationName { get; set; }
    public bool BypassCache { get; set; }
}
