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
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<FolderValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SetValidator>());
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
app.MapGet("/api/folder", [Authorize] async (ISpellItService spellItService) => await spellItService.GetAllFolders());
app.MapGet("/api/folder/{folderid}", [Authorize] async (ISpellItService spellItService, string folderid) => await spellItService.GetFolderById(folderid));
app.MapPost("/api/folder", [Authorize] async (ISpellItService spellItService,IValidator<Folder> validator, Folder folder) =>
{
    var validation = new FolderValidator();
    var result = validation.Validate(folder);
    if (result != null && result.Errors.Count()<0){
        return Results.BadRequest(result.Errors);
    }
        await spellItService.AddFolder(folder);
        return Results.Created($"/api/folder/{folder.Id}", folder);
});
app.MapDelete("/api/folder/{id}",[Authorize] async (ISpellItService spellItService, string id) =>
{
    await spellItService.DeleteFolder(id);
});
app.MapPut("/api/folder",[Authorize] async (ISpellItService spellItService, Folder folder) =>
{
    await spellItService.UpdateFolder(folder);
});

// SET

app.MapGet("/api/set", [Authorize] async  (ISpellItService spellItService) => await spellItService.GetAllSet());
app.MapGet("/api/set/{setid}",[Authorize] async (ISpellItService spellItService, string setid) => await spellItService.GetSetById(setid));
app.MapPost("/api/set",[Authorize] async (ISpellItService spellItService,IValidator<Set> validator, Set set) =>
{
    var validation = new SetValidator();
    var result = validation.Validate(set);
    if (result != null && result.Errors.Count()<0){
        return Results.BadRequest(result.Errors);
    }
        await spellItService.AddSet(set);
        return Results.Created($"/api/set/{set.Id}", set);

    // var errors = result.Errors.Select(e => new {errors = e.ErrorMessage});
    // return Results.BadRequest(errors);
});
app.MapDelete("/api/set/{id}",[Authorize] async (ISpellItService spellItService, string id) =>
{
    await spellItService.DeleteSet(id);
});
app.MapPut("/api/set/word",[Authorize] async (ISpellItService spellItService, Set set) =>
{
    await spellItService.UpdateWordInSet(set);
});


// app.Run("http://0.0.0.0:3000");
app.Run();
public partial class Program { }
