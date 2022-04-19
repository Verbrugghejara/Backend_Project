namespace SpellIt.Models;

public class Set 
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}
    public string? Title { get; set; }
    public string? Author { get; set; }
    public List<Word>? Words { get; set; }
    public int Amount { get{
        return Words.Count;
    }}
}