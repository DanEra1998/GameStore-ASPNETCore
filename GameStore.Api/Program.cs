using GameStore.Api.DTOs;
using GameStore.Api.Endpoints;


var builder = WebApplication.CreateBuilder(args);
// we will activate/register input validation using builder such that we can enforce [Required] field within 
//CreateGameDTO file
builder.Services.AddValidation();
//all validation services will be available for all our endpoints
var app = builder.Build();


app.MapGamesEndpoints();

app.Run();