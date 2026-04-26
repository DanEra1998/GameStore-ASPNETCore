using GameStore.Api.Data;
using GameStore.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GenreEndpoints
{
    public static void MapGenreEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/genres");
        
        //GET endpoint defined to respond to /genres
        group.MapGet("/", async (GameStoreContext dbContext) => 
            await dbContext.Genres
                .Select(genre => new GenreDto(genre.Id, genre.Name))
                .AsNoTracking()
                .ToListAsync());
    }
}