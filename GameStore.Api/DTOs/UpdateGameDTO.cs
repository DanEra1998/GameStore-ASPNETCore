namespace GameStore.Api.DTOs;

public record UpdateGameDTO(
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
    
    );