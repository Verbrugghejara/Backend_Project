namespace SpellIt.Repositories;

public interface IWordRepository
{
    Task<Word> AddWord(Word newWord);
    Task DeleteWord(string id);
    Task<List<Word>> GetAllWords();
    Task<Word> GetWordById(string Id);
    Task<Word> UpdateWord(Word word);
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

    public async Task<Word> GetWordById(string Id)
    {
        return await _context.WordsCollection.Find<Word>(c => c.Id == Id).FirstOrDefaultAsync();
    }
    public async Task<Word> AddWord(Word newWord)
    {
        await _context.WordsCollection.InsertOneAsync(newWord);
        return newWord;
    }
    public async Task<Word> UpdateWord(Word word)
    {
        var filter = Builders<Word>.Filter.Eq("Id", word.Id);
        var update = Builders<Word>.Update.Set("Value", word.value);
        var result = await _context.WordsCollection.UpdateOneAsync(filter, update);
        return await GetWordById(word.Id);
    }

    public async Task DeleteWord(string id)
    {
        var filter = Builders<Word>.Filter.Eq("Id", id);
        var result = await _context.WordsCollection.DeleteOneAsync(filter);
    }

}