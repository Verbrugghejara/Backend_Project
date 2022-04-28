namespace SpellIt.GraphQL.Mutations;

public class Mutation
{
    public record AddWordInput(string value);
    public record AddWordPayload(Word Word);
    public record AddSetInput(string title, string author);
    public record AddSetPayload(Set Set);
    public record AddFolderInput(string title, string author);
    public record AddFolderPayload(Folder Folder);

    public async Task<AddFolderPayload> AddFolder([Service] ISpellItService spellItService, AddFolderInput input)
    {
        var newFolder = new Folder()
        {
            Title = input.title,
            Author = input.author
        };
        var created = await spellItService.AddFolder(newFolder);
        return new AddFolderPayload(created);
    }
    public async Task<AddSetPayload> AddSet([Service] ISpellItService spellItService, AddSetInput input)
    {
        var newSet = new Set()
        {
            Title = input.title,
            Author = input.author
        };
        var created = await spellItService.AddSet(newSet);
        return new AddSetPayload(created);
    }
    public async Task<AddWordPayload> AddWord([Service] ISpellItService spellItService, AddWordInput input)
    {
        var newWord = new Word()
        {
            value = input.value,
        };
        var created = await spellItService.AddWord(newWord);
        return new AddWordPayload(created);
    }
    public async Task UpdateFolder([Service] ISpellItService spellItService, AddFolderInput input)
    {
        var updateFolder = new Folder()
        {
            Title = input.title,
            Author = input.author
        };
        await spellItService.UpdateFolder(updateFolder);
    }
    public async Task UpdateWord([Service] ISpellItService spellItService, AddWordInput input)
    {
        var updateWord = new Word()
        {
            value = input.value,
        };
        await spellItService.UpdateWord(updateWord);
    }
    public async Task UpdateSet([Service] ISpellItService spellItService, AddSetInput input)
    {
        var updateSet = new Set()
        {
            Title = input.title,
            Author = input.author
        };
        await spellItService.UpdateSet(updateSet);
    }
    public async Task DeleteFolder([Service] ISpellItService spellItService, DeleteFolderInput input)
    {
        var deleteFolder = new Folder()
        {
            Id = input.id
        };
        await spellItService.DeleteFolder(deleteFolder.Id);
    }
    public async Task DeleteWordInSet([Service] ISpellItService spellItService, DeleteSetInput input)
    {
        var deleteSet = new Word()
        {
            Id = input.id
        };
        await spellItService.DeleteWordInSet(deleteSet);
    }
    public async Task DeleteWord([Service] ISpellItService spellItService, DeleteWordInput input)
    {
        var deleteWord = new Word()
        {
            Id = input.id
        };
        await spellItService.DeleteWord(deleteWord.Id);
    }
}