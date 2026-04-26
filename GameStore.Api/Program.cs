using GameStore.Api.Data;
using GameStore.Api.DTOs;
using GameStore.Api.Endpoints;
using GameStore.Api.Models;


var builder = WebApplication.CreateBuilder(args);
// we will activate/register input validation using builder such that we can enforce [Required] field within 
//CreateGameDTO file
builder.Services.AddValidation();
//all database logic is within our own created AddGameStoreDB which we extended EF (requires static) 
builder.AddGameStoreDb(); 

//all validation services will be available for all our endpoints
var app = builder.Build();


app.MapGamesEndpoints();
app.MapGenreEndpoints();

app.MigrateDb();

app.Run();