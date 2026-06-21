# AI Theme Generation Flow

This document describes how a user's natural language prompt becomes a playable game theme and rule set.

## Step-by-Step Flow

1. **User Input:** User types "Neon Cyberpunk" in the UI.
2. **C# Trigger:** The `GenerateAIConfig` method is called in `Home.razor`.
3. **Injected Service:** The `IGameConfigurationGenerator` (implemented by `TransformersJsConfigGenerator`) is invoked.
4. **JS Bridge:** C# calls `aiBridge.generateConfig` via JS Interop.
5. **LLM Inference:** `Transformers.js` uses `SmolLM2` to generate a structured JSON string.
6. **JSON Return:** The string is returned to C#.
7. **Deserialization:** C# converts the JSON into `GameConfiguration` (containing `Theme` and `GameRules`).
8. **State Update:** `GameState` and `ThemeManager` are updated.
9. **Render:** The `ThemeRenderer` uses the new colors and handles rule-based logic (like Powerups).

```mermaid
flowchart TD
    Prompt[User Prompt] --> Bridge[IGameConfigurationGenerator]
    Bridge --> LLM[Transformers.js LLM]
    LLM --> JSON[Structured JSON]
    JSON --> CSharp[C# Deserializer]
    CSharp --> Engine[Game Engine]
    Engine --> Renderer[Canvas Renderer]
```
