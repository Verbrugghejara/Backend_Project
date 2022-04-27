namespace SpellIt.Repositories;

public interface ISetRepository
{
    Task<Set> AddSet(Set newSet);
    Task DeleteSet(string id);
    Task<List<Set>> GetAllSet();
    Task<Set> GetSetById(string id);
    Task<Set> UpdateSet(Set set);
}

public class SetRepository : ISetRepository
{
    private readonly IMongoContext _context;
    public SetRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<Set> AddSet(Set newSet)
    {
        await _context.SetsCollection.InsertOneAsync(newSet);
        return newSet;
    }

    public async Task<List<Set>> GetAllSet()
    {
        return await _context.SetsCollection.Find(_ => true).ToListAsync();
    }
    public async Task<Set> GetSetById(string id)
    {
        return await _context.SetsCollection.Find<Set>(id).FirstOrDefaultAsync();
    }
    public async Task<Set> UpdateSet(Set set)
    {
        var filter = Builders<Set>.Filter.Eq("Id", set.Id);
        var update = Builders<Set>.Update.Set("Title", set.Title);
        var result = await _context.SetsCollection.UpdateOneAsync(filter, update);
        return await GetSetById(set.Id);
    }

    public async Task DeleteSet(string id)
    {
        var filter = Builders<Set>.Filter.Eq("Id", id);
        var result = await _context.SetsCollection.DeleteOneAsync(filter);
    }

}