using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;
// inherit for DBContext which is from EF Core
public class GameStoreContext(DbContextOptions<GameStoreContext> options) 
 : DbContext(options) // several parameters including connection string will be recieved as part of the "options"
                      // parameter
{
 //this acts as DB context, in the world of ef core represents a session between api and db
 // and can be used to both query and save instances of entities into db
 //we must define properties that represents the mapping of your objects and database tables 
 public DbSet<Game> Games => Set<Game>(); // property that points into set of game
  public DbSet<Genre> Genres => Set<Genre>(); 
 
 
}