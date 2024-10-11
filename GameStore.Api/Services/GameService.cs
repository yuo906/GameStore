using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Services;

public class GameService
{
    private readonly GameStoreContext _dbContext;
    public GameService(GameStoreContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GameSummaryDto>> GetGameList()
    {
        return await _dbContext.Games
                    // 因為 ToGameSummaryDto 中的 Genre 不為空，所以要先篩掉
                    .Include(game => game.Genre)
                    .Select(game => game.ToGameSummaryDto())
                    // .net 框架會自行追蹤，noTracking 可以提高性能
                    .AsNoTracking()
                    // 避免在查詢大數據集時阻塞應用程式的主執行緒
                    .ToListAsync();
    }

    public async Task<IResult> GetGameDetail(int id)
    {
        // GameDto? game = games.Find(game => game.Id == id);
        Game? game = await _dbContext.Games.FindAsync(id);

        return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
    }

    public async Task<IResult> CreateGame(CreateGameDto newGame, string GetGameEndpointName)
    {
        Game game = newGame.ToEntity();

        _dbContext.Games.Add(game);
        await _dbContext.SaveChangesAsync();

        return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToGameDetailsDto());
    }

    public async Task<IResult> UpdateGame(int id, UpdateGameDto updateGame)
    {
        var existingGame = await _dbContext.Games.FindAsync(id);

        if (existingGame is null)
        {
            return Results.NotFound();
        }

        _dbContext.Entry(existingGame)
                .CurrentValues
                .SetValues(updateGame.ToEntity(id));

        await _dbContext.SaveChangesAsync();

        return Results.NoContent();
    }

    public async Task<IResult> DeleteGame(int id)
    {
        await _dbContext.Games
                    .Where(game => game.Id == id)
                    .ExecuteDeleteAsync();
                    
        return Results.NoContent();
    }

}
