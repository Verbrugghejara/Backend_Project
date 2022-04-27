
//.NET
global using System;
global using Microsoft.Extensions.Options;

// Nuget
global using MongoDB.Bson;
global using MongoDB.Bson.Serialization.Attributes;
global using MongoDB.Driver;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using Microsoft.AspNetCore.Authorization;

//Custumer
global using SpellIt.Models;
global using SpellIt.Configuration;
global using SpellIt.Context;
global using SpellIt.Repositories;
global using SpellIt.Service;
global using SpellIt.Authen.Service;
global using SpellIt.GraphQL.Queries;
global using SpellIt.GraphQL.Mutations;
