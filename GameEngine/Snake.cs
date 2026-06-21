/*
READ THIS FIRST

Purpose:
Represents the snake, its body, movement logic, and growth.

Why this file exists:
Contains the core "intelligence" of the snake's behavior, such as moving and detecting self-collision.

Learning Note:
The snake doesn't know about the UI or the Renderer. It only knows its body segments and how to "step" forward.
*/

using System.Collections.Generic;
using System.Linq;

namespace AISnakeStudio.GameEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class Snake
{
    private readonly List<Point> _body = new();
    public IReadOnlyList<Point> Body => _body;
    public Point Head => _body[0];
    public Direction CurrentDirection { get; private set; } = Direction.Right;
    private Direction _nextDirection = Direction.Right;

    public Snake(Point initialPosition, int initialLength = 3)
    {
        for (int i = 0; i < initialLength; i++)
        {
            _body.Add(new Point(initialPosition.X - i, initialPosition.Y));
        }
    }

    public void SetDirection(Direction direction)
    {
        // Prevent 180-degree turns
        if (direction == Direction.Up && CurrentDirection != Direction.Down) _nextDirection = Direction.Up;
        else if (direction == Direction.Down && CurrentDirection != Direction.Up) _nextDirection = Direction.Down;
        else if (direction == Direction.Left && CurrentDirection != Direction.Right) _nextDirection = Direction.Left;
        else if (direction == Direction.Right && CurrentDirection != Direction.Left) _nextDirection = Direction.Right;
    }

    public void TeleportTo(Point position)
    {
        _body[0] = position;
    }

    public void Move(bool grow)
    {
        CurrentDirection = _nextDirection;
        var newHead = CurrentDirection switch
        {
            Direction.Up => new Point(Head.X, Head.Y - 1),
            Direction.Down => new Point(Head.X, Head.Y + 1),
            Direction.Left => new Point(Head.X - 1, Head.Y),
            Direction.Right => new Point(Head.X + 1, Head.Y),
            _ => Head
        };

        _body.Insert(0, newHead);
        if (!grow)
        {
            _body.RemoveAt(_body.Count - 1);
        }
    }

    public bool CheckSelfCollision()
    {
        return _body.Skip(1).Any(p => p.X == Head.X && p.Y == Head.Y);
    }
}
