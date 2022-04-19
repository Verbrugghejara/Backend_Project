namespace SpellIt.Configuration;
public class DatabaseSettings
{
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
    public string? SetsCollection { get; set; }
    public string? FoldersCollection { get; set; }
    public string? WordsCollection { get; set; }

}
