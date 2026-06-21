using AISnakeGame.GameEngine;
using Xunit;

namespace AISnakeGame.Tests;

public class SnakeTests
{
    [Fact]
    public void Snake_Should_MoveRight_ByDefault()
    {
        // Arrange
        var initialPos = new Point(10, 10);
        var snake = new Snake(initialPos);

        // Act
        snake.Move(false);

        // Assert
        Assert.Equal(new Point(11, 10), snake.Head);
    }

    [Fact]
    public void Snake_Should_Grow_WhenMovingWithGrowTrue()
    {
        // Arrange
        var initialPos = new Point(10, 10);
        var snake = new Snake(initialPos, initialLength: 3);

        // Act
        snake.Move(true);

        // Assert
        Assert.Equal(4, snake.Body.Count);
        Assert.Equal(new Point(11, 10), snake.Head);
    }

    [Fact]
    public void Snake_Should_Not_Turn_180_Degrees()
    {
        // Arrange
        var initialPos = new Point(10, 10);
        var snake = new Snake(initialPos); // Default direction is Right

        // Act
        snake.SetDirection(Direction.Left);
        snake.Move(false);

        // Assert
        // Should still be moving Right because Left is a 180-degree turn
        Assert.Equal(new Point(11, 10), snake.Head);
    }

    [Fact]
    public void Snake_Should_Detect_SelfCollision()
    {
        // Arrange
        var initialPos = new Point(10, 10);
        var snake = new Snake(initialPos, initialLength: 5);

        // Act
        // Move in a circle to hit itself
        // Start: (10,10), (9,10), (8,10), (7,10), (6,10) - Moving Right
        snake.SetDirection(Direction.Down);
        snake.Move(false); // (10,11), (10,10), (9,10), (8,10), (7,10)
        snake.SetDirection(Direction.Left);
        snake.Move(false); // (9,11), (10,11), (10,10), (9,10), (8,10)
        snake.SetDirection(Direction.Up);
        snake.Move(false); // (9,10), (9,11), (10,11), (10,10), (9,10) -> Collision!

        // Assert
        Assert.True(snake.CheckSelfCollision());
    }
}
