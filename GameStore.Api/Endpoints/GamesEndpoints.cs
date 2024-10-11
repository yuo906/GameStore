using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using GameStore.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    // 擴展方法 要加 this
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
                    .WithParameterValidation();
        // 使用nuget套件 MinimalApis.Extensions;

        // GET /games
        group.MapGet("/", async (GameService gameService) =>
            await gameService.GetGameList())
            .WithName("GamesList")
            .WithSummary("獲取遊戲列表")
            .WithTags("Games");

        // GET /games/1
        group.MapGet("/{id}", async (int id, GameService gameService) =>
            await gameService.GetGameDetail(id))
            .WithName(GetGameEndpointName)
            .WithSummary("獲取遊戲內容")
            .WithTags("Games");

        // POST /games
        group.MapPost("/", async (GameService gameService, CreateGameDto newGame) =>
            await gameService.CreateGame(newGame, GetGameEndpointName))
            .WithName("AddGame")
            .WithSummary("新增遊戲")
            .WithTags("Games");

        // PUT /games
        group.MapPut("/{id}", async (int id, UpdateGameDto updateGame, GameService gameService) =>
            await gameService.UpdateGame(id, updateGame))
            .WithName("ModifyGame")
            .WithSummary("修改遊戲內容")
            .WithTags("Games");

        // DELETE /games/1
        group.MapDelete("/{id}", async (int id, GameService gameService) =>
            await gameService.DeleteGame(id))
            .WithName("DeleteGame")
            .WithSummary("刪除遊戲內容")
            .WithTags("Games");

        return group;
    }
}
