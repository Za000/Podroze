namespace Podroze.Models;

public class Place
{
    public string id { get; set; }
    public string entityType { get; set; }
    public Dictionary<string, string> address { get; set; }
    public Dictionary<string, double> position { get; set; }
}