namespace SpellIt.Service;

public interface ISpellItService
{
    Task<Folder> AddFolder(Folder newFolder);
    Task<Set> AddSet(Set newSet);
    Task<List<Folder>> GetAllFolders();
    Task<List<Set>> GetAllSet();
    Task<List<Word>> GetAllWords();
    Task<Folder> GetFolderById(string id);
    Task<Set> GetSetById(string id);
    Task<Word> GetWordById(string id);
    Task UpdateFolder(string id, Set set);
}

public class SpellItService : ISpellItService
{
    private readonly ISetRepository _setRepository;
    private readonly IFolderRepository _folderRepository;
    private readonly IWordRepository _wordRepository;
    public SpellItService(ISetRepository setRepository, IFolderRepository folderRepository, IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
        _folderRepository = folderRepository;
        _setRepository = setRepository;

    }

    public async Task<Folder> AddFolder(Folder newFolder)
    {
        return await _folderRepository.AddFolder(newFolder);
    }
    public async Task<List<Folder>> GetAllFolders()
    {
        return await _folderRepository.GetAllFolders();

    }
    public async Task<Folder> GetFolderById(string id)
    {
        return await _folderRepository.GetFolderById(id);
    }

    public async Task<Set> AddSet(Set newSet)
    {
        return await _setRepository.AddSet(newSet);
    }

    public async Task<List<Set>> GetAllSet()
    {
        return await _setRepository.GetAllSet();
    }
    public async Task<Set> GetSetById(string id)
    {
        return await _setRepository.GetSetById(id);
    }
    public async Task<List<Word>> GetAllWords()
    {
        return await _wordRepository.GetAllWords();
    }

    public async Task<Word> GetWordById(string id)
    {
        return await _wordRepository.GetWordById(id);
    }
    public async Task UpdateFolder(string id, Set set)
    {
        await _folderRepository.UpdateFolder(id, set);
    }
}