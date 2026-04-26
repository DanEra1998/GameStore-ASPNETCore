using GameStore.Api.Models;
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

    public static void AddGameStoreDb(this WebApplicationBuilder builder)
    {
        // research DEPENDENCY INJECTION 
        var connString = builder.Configuration.GetConnectionString("GameStore"); // RECALL IT IS BAD TO HARDCODE
//registration of db context with connString
        builder.Services.AddSqlite<GameStoreContext>(
            connString, 
            optionsAction: options => options.UseSeeding((context, _) =>
            {
                //Data seeding is the process of pre-populating your database with initial data when it's first created.
                //Think of it as the default data your app needs to function.
                if (!context.Set<Genre>().Any())
                {
                    context.Set<Genre>().AddRange(
                        new Genre { Name = "Fighting" },
                        new Genre { Name = "RPG" },
                        new Genre { Name = "Platformer" },
                        new Genre { Name = "Racing" },
                        new Genre { Name = "Sports" },
                        new Genre { Name = "Shooter" },
                        new Genre { Name = "Sandbox" },
                        new Genre { Name = "Puzzle" },
                        new Genre { Name = "Simulation" },
                        new Genre { Name = "Adventure" }
                    );
                    //we also have to save this into the db
                    context.SaveChanges();
                }
            })
        );
// after this we are ready to talk to sqlite db, we can take advantage entity framework migration features

    }
}