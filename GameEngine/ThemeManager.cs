/*
READ THIS FIRST

Purpose:
Manages the collection of available themes and the currently active theme.

Why this file exists:
Provides a central place to register and retrieve themes.

Learning Note:
This manager allows the UI to easily switch between different visual styles.
*/

using System.Collections.Generic;

namespace AISnakeStudio.GameEngine;

public class ThemeManager
{
    private readonly Dictionary<string, Theme> _themes = new();
    public Theme CurrentTheme { get; private set; }

    public ThemeManager()
    {
        // Register default themes
        AddTheme(new Theme { Name = "Nokia Classic", SnakeColor = "#000", SnakeHeadColor = "#000", FoodColor = "#000", BackgroundColor = "#98fb98", GridColor = "rgba(0,0,0,0.1)" });
        AddTheme(new Theme { Name = "Cyberpunk", SnakeColor = "#00ff00", SnakeHeadColor = "#00ffff", FoodColor = "#ff00ff", BackgroundColor = "#000000", GridColor = "rgba(0,255,0,0.05)" });
        AddTheme(new Theme { Name = "Matrix", SnakeColor = "#00ff41", SnakeHeadColor = "#d1ffbd", FoodColor = "#003b00", BackgroundColor = "#0d0208" });
        AddTheme(new Theme { Name = "Space", SnakeColor = "#ffffff", SnakeHeadColor = "#cccccc", FoodColor = "#ffcc00", BackgroundColor = "#0b3d91" });
        AddTheme(new Theme { Name = "D365 Commerce", SnakeColor = "#0078d4", SnakeHeadColor = "#005a9e", FoodColor = "#d83b01", BackgroundColor = "#f3f2f1", GridColor = "#edebe9", TextColor = "#323130" });
        AddTheme(new Theme { Name = "Ghost Mode", SnakeColor = "rgba(255,255,255,0.3)", SnakeHeadColor = "rgba(255,255,255,0.5)", FoodColor = "#ffeb3b", BackgroundColor = "#121212", GridColor = "rgba(255,255,255,0.02)" });

        CurrentTheme = _themes["Nokia Classic"];
    }

    public void AddTheme(Theme theme)
    {
        _themes[theme.Name] = theme;
    }

    public void SetTheme(string name)
    {
        if (_themes.ContainsKey(name))
        {
            CurrentTheme = _themes[name];
        }
    }

    public IEnumerable<string> GetAvailableThemes() => _themes.Keys;
}
