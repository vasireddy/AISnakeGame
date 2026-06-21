/*
READ THIS FIRST

Purpose:
Represents a point in a 2D coordinate system.

Why this file exists:
To provide a simple, immutable structure for coordinates (X, Y) used by Snake, Food, and Board.

Learning Objective:
Understand how to use C# records for simple data structures in game logic.
*/

namespace AISnakeGame.GameEngine;

public record Point(int X, int Y);
