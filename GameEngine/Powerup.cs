/*
READ THIS FIRST

Purpose:
Represents a temporary power-up item that the snake can collect.

Why this file exists:
Extends the game mechanics with data-driven behavior.

Learning Note:
This demonstrates how to add new entities to the engine that are only
active when certain rules are enabled.
*/

namespace AISnakeStudio.GameEngine;

public enum PowerupType
{
    ScoreBoost,
    SpeedDown,
    Ghost
}

public class Powerup
{
    public Point Position { get; }
    public PowerupType Type { get; }

    public Powerup(Point position, PowerupType type)
    {
        Position = position;
        Type = type;
    }
}
