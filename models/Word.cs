namespace SpellIt.Models;
public class Word
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? value { get; set; }
    public int Correct { get; set; }
    public int Mistake { get; set; }
}