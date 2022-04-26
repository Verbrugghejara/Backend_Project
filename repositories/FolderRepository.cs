namespace SpellIt.Repositories;

public interface IFolderRepository
{
    Task<Folder> AddFolder(Folder newFolder);
    Task<List<Folder>> GetAllFolders();
    Task<Folder> GetFolderById(string id);
    Task UpdateFolder(string id, Set set);
}

public class FolderRepository : IFolderRepository
{
    private readonly IMongoContext _context;
    public FolderRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<Folder> AddFolder(Folder newFolder)
    {
        await _context.FoldersCollection.InsertOneAsync(newFolder);
        return newFolder;
    }

    public async Task<List<Folder>> GetAllFolders()
    {
        return await _context.FoldersCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Folder> GetFolderById(string id)
    {
        return await _context.FoldersCollection.Find<Folder>(id).FirstOrDefaultAsync();
    }
    public async Task UpdateFolder(string id, Set set)
    {
        FilterDefinition<Folder> filter = Builders<Folder>.Filter.Eq("Id", id);
        UpdateDefinition<Folder> update = Builders<Folder>.Update.AddToSet<Set>("set", set);
        await _context.FoldersCollection.UpdateOneAsync(filter, update);
        return;
    }

}