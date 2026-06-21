/*
READ THIS FIRST

Purpose:
Interprets the GameRules and applies them to the GameState.

Why this file exists:
Contains the logic for how rules and powerups are actually executed.

Learning Note:
This acts as a "Rule Engine" or "Interpreter". It translates high-level data into engine behavior.
*/

namespace AISnakeGame.GameEngine;

public class RuleInterpreter
{
    public void ApplyRules(GameState state, GameRules rules)
    {
        // Handle Boundary Rules
        if (rules.Teleport)
        {
            Point head = state.Snake.Head;
            Point newHead = head;

            if (head.X < 0) newHead = new Point(state.Board.Width - 1, head.Y);
            else if (head.X >= state.Board.Width) newHead = new Point(0, head.Y);
            else if (head.Y < 0) newHead = new Point(head.X, state.Board.Height - 1);
            else if (head.Y >= state.Board.Height) newHead = new Point(head.X, 0);

            if (newHead != head)
            {
                state.Snake.TeleportTo(newHead);
            }
        }
    }

    public void ApplyPowerup(GameState state, Powerup powerup)
    {
        switch (powerup.Type)
        {
            case PowerupType.ScoreBoost:
                state.AddScore(50);
                break;
            case PowerupType.SpeedDown:
                state.Rules.SpeedMultiplier *= 0.8f;
                break;
            case PowerupType.Ghost:
                state.Rules.GhostMode = true;
                // Note: In a real game, this would be timed.
                // For simplicity here, it lasts until the next game/reset or AI config.
                break;
        }
    }

    public bool IsGameOver(GameState state, GameRules rules)
    {
        // Ghost Mode Easter Egg: Ignore self-collision
        if (!rules.GhostMode && state.Snake.CheckSelfCollision()) return true;

        if (rules.Walls && state.Board.IsOutOfBounds(state.Snake.Head))
        {
            return true;
        }

        return false;
    }
}
