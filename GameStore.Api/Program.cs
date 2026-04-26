using GameStore.Api.Data;
using GameStore.Api.DTOs;
using GameStore.Api.Endpoints;


var builder = WebApplication.CreateBuilder(args);
// we will activate/register input validation using builder such that we can enforce [Required] field within 
//CreateGameDTO file
builder.Services.AddValidation();

// research DEPENDENCY INJECTION 
var connString = "Data Source=GameStore.db";
//registration of db context with connString
builder.Services.AddSqlite<GameStoreContext>(connString);
// after this we are ready to talk to sqlite db, we can take advantage entity framework migration features

//all validation services will be available for all our endpoints
var app = builder.Build();


app.MapGamesEndpoints();

app.Run();