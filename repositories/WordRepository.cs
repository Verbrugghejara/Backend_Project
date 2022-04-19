namespace SpellIt.Repositories;

public interface IWordRepository
{
    Task<List<Word>> GetAllWords();
    Task<Word> GetWordById(string id);
}

public class WordRepository : IWordRepository
{
    private readonly IMongoContext _context;
    public WordRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<List<Word>> GetAllWords()
    {
        return await _context.WordsCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Word> GetWordById(string id)
    {
        return await _context.WordsCollection.Find<Word>(id).FirstOrDefaultAsync();
    }

}