var builder = WebApplication.CreateBuilder(args);
var mongoSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(mongoSettings);
builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<ISetRepository, SetRepository>();
builder.Services.AddTransient<IFolderRepository, FolderRepository>();
builder.Services.AddTransient<IWordRepository, WordRepository>();
builder.Services.AddTransient<ISpellItService, SpellItService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/folder",async (ISpellItService SpellItService, Folder folder)=> 
{
    var result = await SpellItService.AddFolder(folder);
    return Results.Created("",result);
});
app.MapPost("/set",async (ISpellItService SpellItService, Set set)=> 
{
    var result = await SpellItService.AddSet(set);
    return Results.Created("",result);
});

app.MapGet("/folder",async (ISpellItService SpellItService)=>await SpellItService.GetAllFolders());
app.MapGet("/set",async (ISpellItService SpellItService)=>await SpellItService.GetAllSet());
app.MapGet("/word",async (ISpellItService SpellItService)=>await SpellItService.GetAllWords());

app.MapGet("/folder/{folderid}",async (ISpellItService SpellItService,string folderid)=> await SpellItService.GetFolderById(folderid));
app.MapGet("/set/{setid}",async (ISpellItService SpellItService,string setid)=> await SpellItService.GetSetById(setid));
app.MapGet("/word/{wordid}",async (ISpellItService SpellItService,string wordid)=> await SpellItService.GetWordById(wordid));


app.Run("http://0.0.0.0:3000");
