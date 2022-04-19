namespace SpellIt.Models;

public class Folder 
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}
    public string? Title { get; set; }
    public string? Author { get; set; }
    public Set? Set { get; set; }
    public int Amount { get{
        return Set.Title.Length;
    }}
}