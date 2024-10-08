using System;

namespace GameStore.Endpoints;

public static class GamesEndpoints {
  private const string GetGameEndpointName = "GetGame";
  private static readonly List<Contracts.GameContract> games = [
    new (
      1,
      "Street Fighter II",
      "Fighting",
      19.99M,
      new DateOnly(1992, 7, 15)),
    new (
      2,
      "Final Fantasy XIV",
      "Roleplaying",
      59.99M,
      new DateOnly(2010, 9, 30)),
    new (
      3,
      "FIFA 23",
      "Sports",
      69.99M,
      new DateOnly(2022, 9, 27)),
  ];

  public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app) {

    RouteGroupBuilder group = app.MapGroup("games")
      .WithParameterValidation();

    // GET /games
    group.MapGet("/", () => games);

    // GET /games/1
    group.MapGet("/{id}", (int id) => {
      Contracts.GameContract? game = games.Find(game => game.Id == id);

      return game is null ? Results.NotFound() : Results.Ok(game);
    }).WithName(GetGameEndpointName);

    // POST /games
    group.MapPost("/", (Contracts.CreateGameContract newGame) => {
      Contracts.GameContract game = new Contracts.GameContract(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
      );

      games.Add(game);

      return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
    })
    .WithParameterValidation();

    // PUT /games/1
    group.MapPut("/{id}", (int id, Contracts.UpdateGameContract updatedGame) => {
      int index = games.FindIndex(game => game.Id == id);

      if (index == -1) {
        return Results.NotFound();
      }

      games[index] = new Contracts.GameContract (
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
      ); 

      return Results.NoContent();
    });

    // DELETE /games/1
    group.MapDelete("/{id}", (int id) => {
      games.RemoveAll(game => game.Id == id);

      return Results.NoContent();
    });

    return group;
  }
}
