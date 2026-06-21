# JavaScript Interop Deep Dive

In Blazor WebAssembly, C# and JavaScript live in the same browser tab but in different worlds. JS Interop is the bridge between them.

## Blazor to JavaScript

Use `IJSRuntime.InvokeAsync<T>` or `InvokeVoidAsync`.

**Example:**
`await JSRuntime.InvokeVoidAsync("console.log", "Hello from C#!");`

## JavaScript to Blazor

Use `[JSInvokable]` attribute on a C# method.

**Example:**
In JS: `DotNet.invokeMethodAsync('AssemblyName', 'MethodName', args);`

## Sequence in AI Snake Studio

```mermaid
sequenceDiagram
    participant C as C# (Blazor)
    participant J as JS (ai.js)
    participant T as Transformers.js

    C->>J: aiBridge.generateConfig(prompt)
    J->>T: generator(fullPrompt)
    T-->>J: text response
    J-->>J: parse JSON
    J-->>C: return JSON string
    C-->>C: Deserialize to GameConfiguration
```
