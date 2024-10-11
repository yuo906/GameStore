namespace GameStore.Api.Dtos;

public record class GameDetailsDto(
    int Id, 
    string Name, 
    int GenreId, 
    decimal Price, 
    DateOnly ReleaseDate
);
// {
//     public int Id { get; set;}
//     public string Name { get; set;}
//     public string Genre { get; set;}
//     public decimal Price { get; set;}
//     public DateOnly ReleaseDate { get; set;}
// }
