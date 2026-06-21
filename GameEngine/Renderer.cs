/*
READ THIS FIRST

Purpose:
Handles drawing the game state onto an HTML5 Canvas.

Why this file exists:
Separates the visual representation of the game from the underlying logic.
If we wanted to switch from 2D Canvas to WebGL or even DOM, we would only change the Renderer.

Learning Note:
This class uses JS Interop to communicate with the browser's Canvas API.
*/

using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace AISnakeStudio.GameEngine;

public class Renderer
{
    private readonly IJSRuntime _jsRuntime;
    private readonly string _canvasId;
    private readonly int _cellSize;

    public Renderer(IJSRuntime jsRuntime, string canvasId, int cellSize = 20)
    {
        _jsRuntime = jsRuntime;
        _canvasId = canvasId;
        _cellSize = cellSize;
    }

    public async Task RenderAsync(GameState gameState)
    {
        // We'll call a JavaScript function to perform the actual drawing.
        // This is more efficient than making many JS calls from C# for every rectangle.
        await _jsRuntime.InvokeVoidAsync("gameRenderer.draw", _canvasId, new
        {
            snake = gameState.Snake.Body,
            food = gameState.Food.Position,
            cellSize = _cellSize,
            width = gameState.Board.Width * _cellSize,
            height = gameState.Board.Height * _cellSize,
            isGameOver = gameState.IsGameOver,
            score = gameState.Score
        });
    }
}
