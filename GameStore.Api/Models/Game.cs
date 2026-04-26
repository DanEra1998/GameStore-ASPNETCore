using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Models;

public class Game
{
    //we will introduce property of the game model
    public int Id { get; set; }
    
    public required string Name { get; set; } 
    // we will have many genres so we can not just define a genre variable, rather we should make a class
    public Genre? Genre { get; set; } //composition property has "a game "HAS A" genre" also, ? means can be nullable
    //recall we want to create an ORM in the long scheme of things so we need to remember our primary keys, foreign keys
    public int GenreId { get; set; }// we must always have a genreId associated with every Game record, meaning association is required, not optional
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
}