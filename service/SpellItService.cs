namespace SpellIt.Service;

public interface ISpellItService
{
    Task<Folder> AddFolder(Folder newFolder);
    Task<Set> AddSet(Set newSet);
    Task<Word> AddWord(Word newWord);
    Task DeleteFolder(string id);
    Task DeleteSet(string id);
    Task DeleteWord(string id);
    Task<List<Folder>> GetAllFolders();
    Task<List<Set>> GetAllSet();
    Task<List<Word>> GetAllWords();
    Task<Folder> GetFolderById(string id);
    Task<Set> GetSetById(string id);
    Task<Word> GetWordById(string id);
    Task UpdateFolder(Folder folder);
    Task<Set> UpdateSet(Set set);
    Task UpdateWord(Word word);
    Task UpdateWordInSet(Set set);
    Task UpdateWordsInSet(Set set);
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
        if (newFolder == null)
            throw new ArgumentException();
        else
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
    public async Task<Word> AddWord(Word newWord)
    {
        return await _wordRepository.AddWord(newWord);
    }
    public async Task UpdateFolder(Folder folder)
    {
        await _folderRepository.UpdateFolder(folder);
    }

    public async Task DeleteFolder(string id)
    {
        await _folderRepository.DeleteFolder(id);
    }
    public async Task DeleteSet(string id)
    {
        await _setRepository.DeleteSet(id);
    }
    public async Task UpdateWordsInSet(Set set)
    {
        await _setRepository.UpdateWordsInSet(set);
    }
    public async Task UpdateWordInSet(Set set)
    {
        await _setRepository.UpdateWordInSet(set);
    }

    public async Task UpdateWord(Word word)
    {
        await _wordRepository.UpdateWord(word);
    }
    public async Task<Set> UpdateSet(Set set)
    {
        await _setRepository.UpdateSet(set);
        return set;
    }

    public async Task DeleteWord(string id)
    {
        await _wordRepository.DeleteWord(id);
    }
    // public async Task DeleteWordInSet(string id)
    // {
    //     await _setRepository.DeleteWordInSet(id);
    // }
}