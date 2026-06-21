# Project Architecture

AI Snake Studio is built with a clean separation of concerns, following the Open/Closed Principle and Data-Driven Design.

## Component Responsibilities

* **GameEngine:** Core logic (Snake, Food, Board, Rules). Completely decoupled from UI.
* **Renderer:** Interprets `GameState` and draws it to the Canvas.
* **ThemeManager:** Handles visual styles.
* **RuleInterpreter:** Applies high-level rules to engine behavior.
* **TransformersJsConfigGenerator:** Orchestrates AI inference.
* **ai.js:** The JavaScript host for Transformers.js.

## Folder Structure

* `GameEngine/`: The "Heart" of the application (C# logic).
* `Pages/`: Blazor components (UI).
* `wwwroot/js/`: JavaScript logic (Renderer, AI).
* `docs/`: Educational reference material.
* `tests/`: xUnit tests for engine logic.

## Execution Flow

```mermaid
graph TD
    UI[Home.razor] --> Engine[GameState]
    UI --> Renderer[ThemeRenderer]
    UI --> AI[TransformersJsConfigGenerator]
    AI --> JS[ai.js]
    JS --> Trans[Transformers.js]
    Engine --> Rules[RuleInterpreter]
    Renderer --> Canvas[HTML5 Canvas]
```

## Dependency Graph

```mermaid
classDiagram
    class GameState {
        +Snake snake
        +Food food
        +GameRules rules
        +Update()
    }
    class Snake {
        +Move()
        +TeleportTo()
    }
    class RuleInterpreter {
        +ApplyRules()
    }
    class ThemeRenderer {
        +RenderAsync()
    }

    GameState *-- Snake
    GameState *-- RuleInterpreter
    ThemeRenderer ..> GameState : visualizes
```
