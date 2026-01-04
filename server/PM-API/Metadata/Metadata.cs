using System.Text.Json.Serialization;

namespace PM_API.Metadata;

public class Metadata
{
    public const string LinkRelationNext = "next";
    public const string LinkRelationPrev = "prev";

    [JsonPropertyName("location")]
    public string Location { get; set; }
    
    // Allowed methods for the location of this resource
    [JsonPropertyName("AllowedMethods")]
    public List<string> AllowedMethods { get; set; }
    
    [JsonPropertyName("links")]
    public List<Link> Links { get; set; }
}

public class Link
{
    [JsonPropertyName("rel")]
    public string Rel { get; set; }
    
    [JsonPropertyName("location")]
    public string Location { get; set; }
    
    [JsonPropertyName("allow")] 
    public List<string> AllowedMethods { get; set; } = new List<string>();
       
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
        Link link = (Link) obj;
        return (Rel == link.Rel) && (Location == link.Location);
    }
        
    public override int GetHashCode()
    {
        return Tuple.Create(Rel, Location).GetHashCode();
    }
}
