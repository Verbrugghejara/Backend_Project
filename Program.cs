using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var mongoSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(mongoSettings);
var authSettings = builder.Configuration.GetSection("AuthenticationSettings");
builder.Services.Configure<AuthenticationSettings>(authSettings);
builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<ISetRepository, SetRepository>();
builder.Services.AddTransient<IFolderRepository, FolderRepository>();
builder.Services.AddTransient<IWordRepository, WordRepository>();
builder.Services.AddTransient<ISpellItService, SpellItService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization(options =>
{

});
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["AuthenticationSettings:Issuer"],
        ValidAudience = builder.Configuration["AuthenticationSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(builder.Configuration["AuthenticationSettings:SecretForKey"]))
    };
});
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddGraphQLServer()
    .AddQueryType<Queries>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
    .AddMutationType<Mutation>();
var app = builder.Build();
app.MapSwagger();
app.UseSwaggerUI();
app.MapGraphQL();
app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/authentication", (IAuthenticationService authenticationService, IOptions<AuthenticationSettings> authSettings, AuthenticationRequestBody authenticationRequestBody) =>
{
    var user = authenticationService.ValidateUser(authenticationRequestBody.username, authenticationRequestBody.password);
    if (user == null)
        return Results.Unauthorized();

    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(authSettings.Value.SecretForKey));
    var signingCredentails = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    var claimsForToken = new List<Claim>();
    claimsForToken.Add(new Claim("sub", "1"));
    claimsForToken.Add(new Claim("given_name", user.name));
    claimsForToken.Add(new Claim("city", user.city));

    var jwtSecurityToken = new JwtSecurityToken(
        authSettings.Value.Issuer,
        authSettings.Value.Audience,
        claimsForToken,
        DateTime.UtcNow,
        DateTime.UtcNow.AddHours(500),
        signingCredentails

    );
    var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

    return Results.Ok(tokenToReturn);
});

// app.MapGet("/swagger/index.html", () => "Hello World!");
// FOLDER
app.MapGet("/folder", [Authorize] async (ISpellItService SpellItService) => await SpellItService.GetAllFolders());
app.MapGet("/folder/{folderid}", [Authorize] async (ISpellItService SpellItService, string folderid) => await SpellItService.GetFolderById(folderid));
app.MapPost("/folder", [Authorize] async (ISpellItService SpellItService, Folder folder) =>
{
    var result = await SpellItService.AddFolder(folder);
    return Results.Created("/folder/"+folder.Id, result);
});
app.MapDelete("/folder/{id}",[Authorize] async (ISpellItService spellItService, string id) =>
{
    await spellItService.DeleteFolder(id);
});
app.MapPut("/folder",[Authorize] async (ISpellItService spellItService, Folder folder) =>
{
    await spellItService.UpdateFolder(folder);
});

// SET

app.MapGet("/set", [Authorize] async  (ISpellItService SpellItService) => await SpellItService.GetAllSet());
app.MapGet("/set/{setid}",[Authorize] async (ISpellItService SpellItService, string setid) => await SpellItService.GetSetById(setid));
app.MapPost("/set",[Authorize] async (ISpellItService SpellItService, Set set) =>
{
    var result = await SpellItService.AddSet(set);
    return Results.Created("/set/"+set.Id, result);
});
app.MapDelete("/set/{id}",[Authorize] async (ISpellItService spellItService, string id) =>
{
    await spellItService.DeleteSet(id);
});
app.MapPut("/set/word",[Authorize] async (ISpellItService spellItService, Set set) =>
{
    await spellItService.UpdateWordInSet(set);
});
// app.MapDelete("/wordinset/{wordId}",[Authorize] async (ISpellItService spellItService, string wordId) =>
// {
//     await spellItService.DeleteWordInSet(wordId);
// });

// WORD 
// app.MapGet("/word",[Authorize] async (ISpellItService SpellItService) => await SpellItService.GetAllWords());

// app.MapGet("/word/{wordid}",[Authorize] async (ISpellItService SpellItService, string wordid) => await SpellItService.GetWordById(wordid));
// app.MapDelete("/word/{word}",[Authorize] async (ISpellItService spellItService, string id) =>
// {
//     await spellItService.DeleteWord(id);
// });
// app.MapPut("/word",[Authorize] async (ISpellItService spellItService, Word word) =>
// {
//     await spellItService.UpdateWord(word);
// });


app.Run("http://0.0.0.0:3000");
// app.Run();
public partial class Program { }
