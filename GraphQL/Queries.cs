namespace SpellIt.GraphQL.Queries;

public class Queries{
    public async Task<List<Set>> GetSets([Service] SpellItService spellItService) => await spellItService.GetAllSet();
    public async Task<List<Folder>> GetFolders([Service] SpellItService spellItService) => await spellItService.GetAllFolders();
    public async Task<List<Word>> GetWords([Service] SpellItService spellItService) => await spellItService.GetAllWords();
    public async Task<Word> GetWord([Service] SpellItService spellItService, string wordId) => await spellItService.GetWordById(wordId);
    public async Task<Set> GetSet([Service] SpellItService spellItService, string setId) => await spellItService.GetSetById(setId);
    public async Task<Folder> GetFolder([Service] SpellItService spellItService, string folderId) => await spellItService.GetFolderById(folderId);

}
