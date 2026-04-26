using GameStore.Api.Data;
using GameStore.Api.DTOs;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

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
        group.MapGet("/", async (GameStoreContext dbContext) => await dbContext.Games.
            Include(games => games.Genre).Select(game => new GameDto(
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate)).AsNoTracking().ToListAsync());


        // GET /games/id
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) => {
                var game = await dbContext.Games.FindAsync(id);
    
                return game is null ? Results.NotFound() : Results.Ok(new GameDetailsDTO(
                    game.Id,
                    game.Name,
                    game.GenreId,
                    game.Price,
                    game.ReleaseDate
                    ));
            })
            .WithName(GetGameEndpointName);


        //Post endpoint targeting /games, we will need a new DTO to represent incoming payload
        group.MapPost("/", async (CreateGameDTO newGame, GameStoreContext dbContext) =>
        //when MapPost is invoked, dbContext brand new instance created and terminates at end of this method
        {
            // ** this would work as input validation but you would need to do it to every post/put which 
            // becomes tedious an complicated, HANDLE THIS BETTER WITHIN THE DTO
            // if (string.IsNullOrEmpty(newGame.Name))
            // {
            //     return Results.BadRequest("name is required ");
            // }
            
            //we define game model to insert into the database
            Game game = new()
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };
            
            // this line does not actually send game into the db, only ask EF core to keep track
            //that a new game needs to be inserted into the db
            dbContext.Games.Add(game);
            
            //now we actually insert into the db
            // dbContext.SaveChanges();
            //SaveChange() returns an int and we can not use resources at run time, we will move to async version which 
            //returns a task but we now must modify our DTO
            await dbContext.SaveChangesAsync();
            
            GameDetailsDTO gameDto = new(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );
            //return proper response to the client and the payload 
            return Results.CreatedAtRoute(GetGameEndpointName, new {id = gameDto.Id}, gameDto);
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