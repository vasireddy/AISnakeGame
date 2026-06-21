/*
READ THIS FIRST

Purpose:
Implements IAIThemeGenerator (now acting as IAIConfigGenerator) via JS Interop.
*/

using Microsoft.JSInterop;
using System.Text.Json;
using System.Threading.Tasks;

namespace AISnakeStudio.GameEngine;

public class TransformersJsConfigGenerator
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
            return null;
        }
    }

    public async Task<string> GetBackendInfoAsync()
    {
        return await _jsRuntime.InvokeAsync<string>("aiBridge.getBackendInfo");
    }
}
