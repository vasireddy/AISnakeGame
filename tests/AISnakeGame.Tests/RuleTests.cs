using AISnakeStudio.GameEngine;
using Xunit;

namespace AISnakeStudio.Tests;

public class RuleTests
{
    [Fact]
    public void RuleInterpreter_Should_TeleportSnake_WhenTeleportIsEnabled()
    {
        // Arrange
        var state = new GameState(10, 10);
        state.Rules = new GameRules { Teleport = true, Walls = false };

        // Move snake to the right edge (5,5) -> (9,5)
        for (int i = 0; i < 4; i++) state.Update();
        Assert.Equal(new Point(9, 5), state.Snake.Head);

        // Act
        state.Update(); // Should move to (10, 5) then teleport to (0, 5)

        // Assert
        Assert.Equal(new Point(0, 5), state.Snake.Head);
        Assert.False(state.IsGameOver);
    }

    [Fact]
    public void RuleInterpreter_Should_EndGame_WhenWallsAreEnabled()
    {
        // Arrange
        var state = new GameState(10, 10);
        state.Rules = new GameRules { Walls = true, Teleport = false };

        // Act
        for (int i = 0; i < 5; i++) state.Update();

        // Assert
        Assert.True(state.IsGameOver);
    }
}
