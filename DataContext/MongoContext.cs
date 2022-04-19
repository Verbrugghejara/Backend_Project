namespace SpellIt.Context;

public interface IMongoContext
{
    IMongoClient Client { get; }
    IMongoDatabase Database { get; }
    IMongoCollection<Word> WordsCollection { get; }
    IMongoCollection<Set> SetsCollection { get; }
    IMongoCollection<Folder> FoldersCollection { get; }
}

public class MongoContext : IMongoContext
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    private readonly DatabaseSettings _settings;

    public IMongoClient Client
    {
        get
        {
            return _client;
        }
    }
    public IMongoDatabase Database => _database;

    public MongoContext(IOptions<DatabaseSettings> dbOptions)
    {
        _settings = dbOptions.Value;
        _client = new MongoClient(_settings.ConnectionString);
        _database = _client.GetDatabase(_settings.DatabaseName);
    }

    public IMongoCollection<Word> WordsCollection
    {
        get
        {
            return _database.GetCollection<Word>(_settings.WordsCollection);
        }
    }
    public IMongoCollection<Set> SetsCollection
    {
        get
        {
            return _database.GetCollection<Set>(_settings.SetsCollection);
        }
    }
    public IMongoCollection<Folder> FoldersCollection
    {
        get
        {
            return _database.GetCollection<Folder>(_settings.FoldersCollection);
        }
    }

}