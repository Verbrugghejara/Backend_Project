namespace SpellIt.Models;

public class Set
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Language { get; set; }
    public List<Word>? Words { get; set; }
    public int CountOfWords
    {
        get
        {
            if (Words?.Count() == null)
            {
                return 0;
            }
            else
            {
                return Words.Count();
            }
        }
    }
}
