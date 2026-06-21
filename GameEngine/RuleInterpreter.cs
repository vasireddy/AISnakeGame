/*
READ THIS FIRST

Purpose:
Interprets the GameRules and applies them to the GameState.

Why this file exists:
Contains the logic for how rules like "Teleport" or "Walls" are actually executed.

Learning Note:
This acts as a "Rule Engine" or "Interpreter". It translates high-level data (JSON) into low-level engine behavior.
*/

namespace AISnakeStudio.GameEngine;

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
