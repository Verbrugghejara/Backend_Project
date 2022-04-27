namespace SpellIt.Models;
 [BsonIgnoreExtraElements]
public class Folder 
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}
    public string? Title { get; set; }
    public string? Author { get; set; }
    public Set[]? Sets { get; set; }
    public int CountOfSets { get{
        if (Sets?.Length == null){
            return 0;
        }else{
        return Sets.Length;

        }
        
    }}
}