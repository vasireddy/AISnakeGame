/*
READ THIS FIRST

Purpose:
Defines the interface for generating themes using AI.

Why this file exists:
By using an interface, we can swap different AI models or even a mock implementation without
changing the rest of the application. This is the Dependency Inversion Principle.

Learning Note:
This allows us to experiment with different LLMs (SmolLM, Phi, etc.) while keeping the UI code clean.
*/

using System.Threading.Tasks;

namespace AISnakeStudio.GameEngine;

public interface IAIThemeGenerator
{
    Task<Theme?> GenerateThemeAsync(string prompt);
    Task<string> GetBackendInfoAsync();
}
