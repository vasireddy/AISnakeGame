# Reference Walkthrough

Welcome, C# Developer! This guide will help you navigate the AI Snake Studio codebase and understand how all the pieces fit together.

## 1. Application Startup
The journey starts in `Program.cs`. This is a standard .NET 9/10 entry point. Note that there is no `Startup.cs`; everything is configured in `Program.cs`.

## 2. WASM Initialization
When the browser hits `index.html`, it loads `_framework/blazor.webassembly.js`. This script downloads the .NET runtime and your app's DLLs. You can see this in the **Network tab** of your browser DevTools.

## 3. Blazor Boot Process
Once the runtime is ready, it calls the `Main` method in `Program.cs`. Blazor then renders the `App.razor` component, which uses `MainLayout.razor` to host our `Home.razor` page.

## 4. JS Interop Call
In `Home.razor`, look at `GenerateAIConfig`. It calls `_aiGenerator.GenerateConfigAsync`.
Trace this to `TransformersJsConfigGenerator.cs`. It uses `IJSRuntime` to call `aiBridge.generateConfig` in `ai.js`.

## 5. Transformers.js Inference
Open `wwwroot/js/ai.js`. This is where the AI magic happens.
* It downloads the `SmolLM2` model (about 360M parameters, ~700MB).
* It uses `navigator.gpu` to detect if WebGPU is available.
* It sends your prompt to the LLM and waits for a JSON response.

## 6. JSON Response & Theme Creation
The LLM returns a JSON string. C# receives this string and uses `JsonSerializer.Deserialize` to turn it into a `GameConfiguration` object.
This object is then fed into the `ThemeManager` and `GameState`.

## 7. Game Rendering
The game loop in `Home.razor` runs every 150ms. Each "tick":
1. `GameState.Update()` moves the snake and checks rules.
2. `ThemeRenderer.RenderAsync()` sends the state and current theme to `gameRenderer.js`.
3. `gameRenderer.js` uses the standard HTML5 `CanvasRenderingContext2D` to draw the pixels.

## Pro Tip for C# Devs
Think of `ai.js` as a "Microservice" that happens to run in the same process. You talk to it via an "RPC-like" bridge (JS Interop).
