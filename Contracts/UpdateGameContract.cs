using System.ComponentModel.DataAnnotations;

namespace GameStore.Contracts;

public record class UpdateGameContract (
  [Required][StringLength(64)] string Name, 
  [Required][StringLength(32)] string Genre, 
  [Range(1,100)] decimal Price,
  DateOnly ReleaseDate
);
