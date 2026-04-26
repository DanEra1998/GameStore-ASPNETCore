using GameStore.Api.DTOs;
namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
   
    const string GetGameEndpointName = "GetGame";
    //this class is the one to contain all API endpoints using Extension methods which live in statis class
    private static readonly List<GameDto> games = [
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
    
    //defining out extension method, all extension methods are static
    //need to define what exaclty we are going to extend, meaning, we are going to add new methods to the object passed
    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        //bring in all of your endpoints
        //GET will be return all games found within /games

        //two parameters, first parameter, is pattern that is matched by definition of MapGet
        //we modify first parameter to our /games path, second parameter is known as the Handler. 
        // Handler tells ASP core what to do when a request is found and matched to what is on left side
        group.MapGet("/", () => games);


        // GET /games/id
        group.MapGet("/{id}", (int id) => {
                var game = games.Find(game => game.Id == id);
    
                return game is null ? Results.NotFound() : Results.Ok(game);
            })
            .WithName(GetGameEndpointName);


        //Post endpoint targeting /games, we will need a new DTO to represent incoming payload
        group.MapPost("/", (CreateGameDTO newGame) =>
        {
            // ** this would work as input validation but you would need to do it to every post/put which 
            // becomes tedious an complicated, HANDLE THIS BETTER WITHIN THE DTO
            // if (string.IsNullOrEmpty(newGame.Name))
            // {
            //     return Results.BadRequest("name is required ");
            // }
            
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            //return proper response to the client and the payload 
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game); 
            // instead of passing entire GET call, name it and pass the name, in our case "GetGame" represents the GET api
    
        });

        // PUT /games/ has to include the id to update the specific game
        // we will need our own DTO

        group.MapPut("/{id}", (int id, UpdateGameDTO updatedGame) => {  
            var index = games.FindIndex(game => game.Id == id);
            //you could create entry if it does not exist, it depends on who you ask as an engineer
            if (index == -1) return Results.NotFound();
    
            games[index] = new GameDto(id, updatedGame.Name, updatedGame.Genre, updatedGame.Price, updatedGame.ReleaseDate);
            return Results.NoContent();
        });
        // DELETE call for game to delete, requires the id endpoint, we do not need a DTO because there is no 
        // data to send, only recieves the id to delete 
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
    
            return Results.NoContent();
        });

    }
}