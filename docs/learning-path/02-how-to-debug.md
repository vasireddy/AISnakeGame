# How to Debug AI Snake Studio

Debugging a Blazor WASM app is different from debugging a standard ASP.NET app. You are debugging code running inside the browser's virtual machine.

## 1. C# Debugging
You can debug C# directly in the browser if you use Visual Studio or VS Code with the Blazor DevTools.
* Set breakpoints in `GameState.cs`.
* Inspect variables just like in a desktop app.

## 2. Console Logging
We use `Console.WriteLine` in C# and `console.log` in JavaScript.
* Open Browser DevTools (**F12** or **Cmd+Option+I**).
* Look at the **Console** tab.
* You will see initialization messages from both Blazor and Transformers.js.

## 3. JS Interop Debugging
If a call to the AI fails:
1. Check the Console for "AI JSON Parse Error".
2. This usually means the LLM didn't follow the JSON format perfectly.
3. You can see the raw output of the LLM by adding a `console.log(text)` in `ai.js`.

## 4. Performance Profiling
* Use the **Performance** tab in DevTools.
* Record a session while the game is running.
* Look for "Long Tasks". If the game stutters during AI generation, it's because the LLM is hogging the CPU/GPU.
