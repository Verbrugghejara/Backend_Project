namespace SpellIt.GraphQL.Mutations;
public record AddSetInput(string title,string Author);
public record DeleteSetInput(string id);