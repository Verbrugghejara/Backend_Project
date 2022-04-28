namespace SpellIt.Models;
 [BsonIgnoreExtraElements]
public class Folder 
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}
    public string? Title { get; set; }
    public string? Author { get; set; }
    public List<Set>? Sets { get; set; }
    public int CountOfSets { get{
        if (Sets?.Count() == null){
            return 0;
        }else{
        return Sets.Count();

        }
        
    }}
}