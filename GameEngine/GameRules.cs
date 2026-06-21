/*
READ THIS FIRST

Purpose:
Defines the gameplay configuration that can be modified by the AI.

Why this file exists:
Isolates gameplay rules from the core engine, allowing the AI to "rewrite" the rules
without changing the source code.

Learning Note:
This is part of a "Data-Driven Design" where the engine behavior is controlled by data structures.
*/

namespace AISnakeGame.GameEngine;

public class GameRules
{
    public bool Walls { get; set; } = true;
    public bool Teleport { get; set; } = false;
    public bool Powerups { get; set; } = false;
    public float SpeedMultiplier { get; set; } = 1.0f;
    public bool GhostMode { get; set; } = false; // Easter Egg: Ignore self-collision
}
