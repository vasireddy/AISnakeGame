/*
READ THIS FIRST

Purpose:
Represents the food item that the snake consumes.

Why this file exists:
Stores the current position of the food on the board.

Learning Objective:
Isolating game entities. Food is a simple entity with a position.
*/

namespace AISnakeGame.GameEngine;

public class Food
{
    public Point Position { get; private set; }

    public Food(Point initialPosition)
    {
        Position = initialPosition;
    }

    public void Respawn(Point newPosition)
    {
        Position = newPosition;
    }
}
