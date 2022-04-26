namespace SpellIt.Models;

public class Folder 
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}
    public string? Title { get; set; }
    public string? Author { get; set; }
    public Set? Set { get; set; }
    public int CountOfSets { get{
        if (Set.Title == null){
            return 0;
        }else{
        return Set.Title.Length;

        }
        
    }}
}