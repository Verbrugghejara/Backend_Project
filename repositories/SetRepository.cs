namespace SpellIt.Repositories;

public interface ISetRepository
{
    Task<Set> AddSet(Set newSet);
    Task<List<Set>> GetAllSet();
    Task<Set> GetSetById(string id);
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

}