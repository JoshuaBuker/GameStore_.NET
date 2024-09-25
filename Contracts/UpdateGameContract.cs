namespace GameStore.Contracts;

public record class UpdateGameContract (
  string Name, 
  string Genre, 
  decimal Price,
  DateOnly ReleaseDate
);
