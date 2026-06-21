/*
READ THIS FIRST

Purpose:
Implements IAIThemeGenerator by communicating with Transformers.js via JS Interop.

Why this file exists:
Acts as the bridge between the C# Blazor world and the JavaScript AI world.

Learning Note:
Blazor WASM cannot run Transformers.js directly (it's a JS library), so we use IJSRuntime
to invoke functions in ai.js.
*/

using Microsoft.JSInterop;
using System.Text.Json;
using System.Threading.Tasks;

namespace AISnakeStudio.GameEngine;

public class TransformersJsThemeGenerator : IAIThemeGenerator
{
    private readonly IJSRuntime _jsRuntime;
    private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

    public TransformersJsThemeGenerator(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<Theme?> GenerateThemeAsync(string prompt)
    {
        var jsonResult = await _jsRuntime.InvokeAsync<string>("aiBridge.generateTheme", prompt);
        if (string.IsNullOrEmpty(jsonResult)) return null;

        try
        {
            return JsonSerializer.Deserialize<Theme>(jsonResult, _jsonOptions);
        }
        catch
        {
            return null;
        }
    }

    public async Task<string> GetBackendInfoAsync()
    {
        return await _jsRuntime.InvokeAsync<string>("aiBridge.getBackendInfo");
    }
}
