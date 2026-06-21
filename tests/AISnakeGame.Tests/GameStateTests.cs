using AISnakeGame.GameEngine;
using Xunit;

namespace AISnakeGame.Tests;

public class GameStateTests
{
    [Fact]
    public void GameState_Should_Detect_OutOfBounds()
    {
        // Arrange
        var gameState = new GameState(10, 10);

        // Act
        // Move snake right until it hits the wall
        // Initial head is at (5,5). Board width is 10 (indices 0-9).
        // Moves: 5->6, 6->7, 7->8, 8->9, 9->10 (out)
        for (int i = 0; i < 5; i++)
        {
            gameState.Update();
        }

        // Assert
        Assert.True(gameState.IsGameOver);
    }
}
