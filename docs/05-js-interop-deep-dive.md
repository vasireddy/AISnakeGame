# JavaScript Interop Deep Dive

In Blazor WebAssembly, C# and JavaScript live in the same browser tab but in different worlds. JS Interop is the bridge between them.

## Blazor to JavaScript

Use `IJSRuntime.InvokeAsync<T>` or `InvokeVoidAsync`.

**Example:**
`await JSRuntime.InvokeVoidAsync("console.log", "Hello from C#!");`

## JavaScript to Blazor

Use `[JSInvokable]` attribute on a C# method.

## Sequence in AI Snake Studio

We use Dependency Injection to manage the AI connection. The UI depends on the `IGameConfigurationGenerator` interface, which is implemented by the `TransformersJsConfigGenerator`.

```mermaid
sequenceDiagram
    participant UI as Home.razor (C#)
    participant C as TransformersJsConfigGenerator (C#)
    participant J as JS (ai.js)
    participant T as Transformers.js

    UI->>C: GenerateConfigAsync(prompt)
    C->>J: aiBridge.generateConfig(prompt)
    J->>T: generator(fullPrompt)
    T-->>J: text response
    J-->>J: parse JSON
    J-->>C: return JSON string
    C-->>C: Deserialize to GameConfiguration
    C-->>UI: GameConfiguration object
```
