using AISnakeStudio.GameEngine;
using Xunit;

namespace AISnakeStudio.Tests;

public class ThemeTests
{
    [Fact]
    public void ThemeManager_Should_SetThemeCorrectly()
    {
        // Arrange
        var manager = new ThemeManager();
        var initialTheme = manager.CurrentTheme.Name;

        // Act
        manager.SetTheme("Cyberpunk");

        // Assert
        Assert.NotEqual(initialTheme, manager.CurrentTheme.Name);
        Assert.Equal("Cyberpunk", manager.CurrentTheme.Name);
        Assert.Equal("#00ff00", manager.CurrentTheme.SnakeColor);
    }

    [Fact]
    public void ThemeManager_Should_AllowAddingNewThemes()
    {
        // Arrange
        var manager = new ThemeManager();
        var newTheme = new Theme { Name = "TestTheme", BackgroundColor = "#ffffff" };

        // Act
        manager.AddTheme(newTheme);
        manager.SetTheme("TestTheme");

        // Assert
        Assert.Equal("TestTheme", manager.CurrentTheme.Name);
        Assert.Equal("#ffffff", manager.CurrentTheme.BackgroundColor);
    }
}
