/*
READ THIS FIRST

Purpose:
Extends the Renderer to handle theme-specific drawing.

Why this file exists:
Encapsulates the logic of "How to draw a theme" separately from "What to draw" (GameState).

Learning Note:
This demonstrates the Open/Closed Principle.
*/

using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace AISnakeStudio.GameEngine;

public class ThemeRenderer
{
    private readonly IJSRuntime _jsRuntime;
    private readonly string _canvasId;
    private readonly int _cellSize;

    public ThemeRenderer(IJSRuntime jsRuntime, string canvasId, int cellSize = 20)
    {
        _jsRuntime = jsRuntime;
        _canvasId = canvasId;
        _cellSize = cellSize;
    }

    public async Task RenderAsync(GameState gameState, Theme theme)
    {
        await _jsRuntime.InvokeVoidAsync("gameRenderer.draw", _canvasId, new
        {
            snake = gameState.Snake.Body,
            food = gameState.Food.Position,
            powerup = gameState.ActivePowerup != null ? new {
                pos = gameState.ActivePowerup.Position,
                type = gameState.ActivePowerup.Type.ToString()
            } : null,
            cellSize = _cellSize,
            width = gameState.Board.Width * _cellSize,
            height = gameState.Board.Height * _cellSize,
            isGameOver = gameState.IsGameOver,
            score = gameState.Score,
            theme = theme
        });
    }
}
