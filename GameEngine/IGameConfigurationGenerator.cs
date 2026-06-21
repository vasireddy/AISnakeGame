/*
READ THIS FIRST

Purpose:
Defines the interface for generating game configurations (Theme + Rules) using AI.

Why this file exists:
By using an interface, we can swap different AI models (SmolLM, Phi, etc.)
without changing the game engine or UI code. This is the Dependency Inversion Principle.

Learning Objective:
Understand how to use interfaces to decouple high-level logic from specific
AI implementations.
*/

using System.Threading.Tasks;

namespace AISnakeStudio.GameEngine;

public interface IGameConfigurationGenerator
{
    Task<GameConfiguration?> GenerateConfigAsync(string prompt);
    Task<string> GetBackendInfoAsync();
}
