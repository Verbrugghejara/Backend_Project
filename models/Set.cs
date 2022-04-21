namespace SpellIt.Models;

public class Set 
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Language { get; set; }
    public List<Word>? Words { get; set; }
    public int? Amount { get{
        if (Id == null){
            return null;
        }else{
        return Words.Count;
        }
    }}
}