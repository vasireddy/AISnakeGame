/*
READ THIS FIRST

Purpose:
Implements IGameConfigurationGenerator by communicating with Transformers.js via JS Interop.

Why this file exists:
Acts as the primary bridge between .NET and the browser-based AI model.

Learning Note:
This implementation handles the complexities of JS Interop, model initialization,
and JSON parsing.
*/

using Microsoft.JSInterop;
using System.Text.Json;
using System.Threading.Tasks;

namespace AISnakeStudio.GameEngine;

public class TransformersJsConfigGenerator : IGameConfigurationGenerator
{
    private readonly IJSRuntime _jsRuntime;
    private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

    public TransformersJsConfigGenerator(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<GameConfiguration?> GenerateConfigAsync(string prompt)
    {
        var jsonResult = await _jsRuntime.InvokeAsync<string>("aiBridge.generateConfig", prompt);
        if (string.IsNullOrEmpty(jsonResult)) return null;

        try
        {
            return JsonSerializer.Deserialize<GameConfiguration>(jsonResult, _jsonOptions);
        }
        catch
        {
            // In a real app, we might retry or try to fix the JSON
            return null;
        }
    }

    public async Task<string> GetBackendInfoAsync()
    {
        return await _jsRuntime.InvokeAsync<string>("aiBridge.getBackendInfo");
    }
}
