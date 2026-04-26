using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.DTOs;

// DTO specifically for incoming POST requests - only contains what the client needs to send
// Id is excluded because the server generates it, not the client
public record CreateGameDTO(
    // [Required] is a Data Annotation - ASP.NET Core will automatically validate this field
    // before your handler even runs. If Name is missing or null in the request body,
    // ASP.NET will return a 400 Bad Request automatically without you writing any if checks BUT 
    // IT IS NOT ENOUGH, NEEDS TO BE ACTIVATED/REGISTER VALIDATION SERVICES SO ASP CORE CAN USE IT
    [Required] [StringLength(50)] string Name, 
    // [Required] [StringLength(20)]  string Genre, 
    // we want to send unique identifer for Genre i.e the id
    [Range(1, 50)] int GenreId,
    [Range(1,100)] decimal Price,
    DateOnly ReleaseDate
);