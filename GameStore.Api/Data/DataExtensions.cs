using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        // we will create a scope which is needed so we can access to instance of gamestoreconext
        //related to dependency injection features of asp net core 
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        // at this point we have access to an instance 
        
        dbContext.Database.Migrate();
    }
}