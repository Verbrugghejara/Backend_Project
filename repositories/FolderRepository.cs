namespace SpellIt.Repositories;

public interface IFolderRepository
{
    Task<Folder> AddFolder(Folder newFolder);
    Task DeleteFolder(string id);
    Task<List<Folder>> GetAllFolders();
    Task<Folder> GetFolderById(string id);
    Task<Folder> UpdateFolder(Folder folder);
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

    public async Task<Folder> GetFolderById(string Id)
    {
        return await _context.FoldersCollection.Find<Folder>(c => c.Id == Id).FirstOrDefaultAsync();
    }
    public async Task<Folder> UpdateFolder(Folder folder)
    {
        var filter = Builders<Folder>.Filter.Eq("Id", folder.Id);
        var update = Builders<Folder>.Update.Set("Sets", folder.Sets);
        var result = await _context.FoldersCollection.UpdateOneAsync(filter, update);
        return await GetFolderById(folder.Id);
    }

    public async Task DeleteFolder(string id)
    {
        var filter = Builders<Folder>.Filter.Eq("Id", id);
        var result = await _context.FoldersCollection.DeleteOneAsync(filter);
    }

}