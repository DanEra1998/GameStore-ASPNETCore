using GameStore.Api.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
    new (1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
    new (2, "The Legend of Zelda: Ocarina of Time", "Adventure", 29.99M, new DateOnly(1998, 11, 21)),
    new (3, "FIFA 24", "Sports", 59.99M, new DateOnly(2023, 9, 29)),
    new (4, "Minecraft", "Sandbox", 26.95M, new DateOnly(2011, 11, 18)),
    new (5, "Halo 3", "Shooter", 19.99M, new DateOnly(2007, 9, 25)),
    new (6, "The Witcher 3", "RPG", 39.99M, new DateOnly(2015, 5, 19)),
    new (7, "Tetris", "Puzzle", 9.99M, new DateOnly(1984, 6, 6)),
    new (8, "Gran Turismo 7", "Racing", 49.99M, new DateOnly(2022, 3, 4)),
    new (9, "Mortal Kombat 11", "Fighting", 19.99M, new DateOnly(2019, 4, 23)),
    new (10, "Stardew Valley", "Simulation", 14.99M, new DateOnly(2016, 2, 26))
];
//GET will be return all games found within /games

//two parameters, first parameter, is pattern that is matched by definition of MapGet
//we modify first parameter to our /games path, second parameter is known as the Handler. 
// Handler tells ASP core what to do when a request is found and matched to what is on left side
app.MapGet("/games", () => games);


// GET /games/id
app.MapGet("/game/{id}", (int id) => games.FirstOrDefault(g => g.Id == id));

app.Run();