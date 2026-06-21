/*
READ THIS FIRST

Purpose:
Manages the overall game state, score, and the interplay between Snake, Food, Board, and Powerups.

Why this file exists:
Serves as the "Controller" or "Orchestrator" of the game logic.

Learning Note:
By centralizing the game state here, we make the game predictable and easier to test.
*/

using System;
using System.Linq;

namespace AISnakeGame.GameEngine;

public class GameState
{
    public Board Board { get; }
    public Snake Snake { get; private set; } = default!;
    public Food Food { get; private set; } = default!;
    public Powerup? ActivePowerup { get; private set; }
    public int Score { get; private set; }
    public bool IsGameOver { get; private set; }
    public GameRules Rules { get; set; } = new();

    private readonly Random _random = new();
    private readonly RuleInterpreter _ruleInterpreter = new();

    public GameState(int width, int height)
    {
        Board = new Board(width, height);
        Reset();
    }

    public void Reset()
    {
        Snake = new Snake(new Point(Board.Width / 2, Board.Height / 2));
        Food = new Food(GenerateRandomPosition());
        ActivePowerup = null;
        Score = 0;
        IsGameOver = false;
    }

    public void Update()
    {
        if (IsGameOver) return;

        bool willGrow = Snake.Head.X == Food.Position.X && Snake.Head.Y == Food.Position.Y;

        Snake.Move(willGrow);

        // Apply rules (e.g., Teleport)
        _ruleInterpreter.ApplyRules(this, Rules);

        if (willGrow)
        {
            Score += 10;
            Food.Respawn(GenerateRandomPosition());

            // Chance to spawn powerup if enabled
            if (Rules.Powerups && ActivePowerup == null && _random.Next(1, 100) > 70)
            {
                SpawnPowerup();
            }
        }

        // Check powerup collection
        if (ActivePowerup != null && Snake.Head.X == ActivePowerup.Position.X && Snake.Head.Y == ActivePowerup.Position.Y)
        {
            _ruleInterpreter.ApplyPowerup(this, ActivePowerup);
            ActivePowerup = null;
        }

        if (_ruleInterpreter.IsGameOver(this, Rules))
        {
            IsGameOver = true;
        }
    }

    private void SpawnPowerup()
    {
        var type = (PowerupType)_random.Next(0, 3);
        ActivePowerup = new Powerup(GenerateRandomPosition(), type);
    }

    private Point GenerateRandomPosition()
    {
        Point pos;
        do
        {
            pos = new Point(_random.Next(0, Board.Width), _random.Next(0, Board.Height));
        } while (IsPositionOccupiedBySnake(pos) || (Food != null && pos == Food.Position));

        return pos;
    }

    private bool IsPositionOccupiedBySnake(Point pos)
    {
        return Snake.Body.Any(p => p.X == pos.X && p.Y == pos.Y);
    }

    public void AddScore(int amount) => Score += amount;
}
