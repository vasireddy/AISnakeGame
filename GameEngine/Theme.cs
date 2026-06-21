/*
READ THIS FIRST

Purpose:
Defines the visual characteristics of the game.

Why this file exists:
By separating theme data from gameplay logic, we adhere to the Open/Closed Principle.
We can add new themes without modifying the GameEngine or the Renderer's core logic.

Learning Note:
This is a "Data-Driven" approach. The Renderer interprets this data to decide how to draw.
*/

namespace AISnakeGame.GameEngine;

public class Theme
{
    public string Name { get; set; } = "Default";
    public string SnakeColor { get; set; } = "#4caf50";
    public string SnakeHeadColor { get; set; } = "#81c784";
    public string FoodColor { get; set; } = "#f44336";
    public string BackgroundColor { get; set; } = "#1a1a1a";
    public string? GridColor { get; set; }
    public string? WallColor { get; set; }
    public string? TextColor { get; set; } = "white";
    public string? FontFamily { get; set; } = "Arial";
}
