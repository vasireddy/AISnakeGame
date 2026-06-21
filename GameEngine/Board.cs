/*
READ THIS FIRST

Purpose:
Defines the dimensions and boundaries of the game play area.

Why this file exists:
Isolates the board size logic from the snake movement and rendering.
It helps in checking if a position is within the valid game area.

Learning Objective:
Separation of Concerns: The board only cares about its size and boundaries.
*/

namespace AISnakeGame.GameEngine;

public class Board
{
    public int Width { get; }
    public int Height { get; }

    public Board(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public bool IsOutOfBounds(Point point)
    {
        return point.X < 0 || point.X >= Width || point.Y < 0 || point.Y >= Height;
    }
}
