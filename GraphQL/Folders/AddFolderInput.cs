namespace SpellIt.GraphQL.Mutations;
public record AddFolderInput(string title,string Author);
public record DeleteFolderInput(string id);