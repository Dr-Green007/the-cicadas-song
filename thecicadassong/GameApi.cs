using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http.Json;
using System.Net.Http;

namespace thecicadassong;

public class GameApi
{
    private readonly HttpClient _http = new() { BaseAddress = new Uri("http://localhost:5000/") };

    public async Task<string?> StartSessionAsync(string name)
    {
        try
        {
            var r = await _http.PostAsJsonAsync("/session/start", name);
            if (!r.IsSuccessStatusCode) return null;
            var doc = await r.Content.ReadFromJsonAsync<SessionDto>();
            return doc?.Token;
        }
        catch (Exception ex) { Log($"session: {ex.Message}"); return null; }
    }

    public async Task<bool> SaveAsync(SaveDto s)
    {
        try
        {
            var r = await _http.PostAsJsonAsync("/save", s);
            return r.IsSuccessStatusCode;
        }
        catch (Exception ex) { Log($"save: {ex.Message}"); return false; }
    }

    public async Task<SaveDto?> LoadAsync(string name)
    {
        try { return await _http.GetFromJsonAsync<SaveDto>($"/save/{name}"); }
        catch (Exception ex) { Log($"load: {ex.Message}"); return null; }
    }

    public async Task<List<SaveDto>?> LeaderboardAsync()
    {
        try { return await _http.GetFromJsonAsync<List<SaveDto>>("/leaderboard"); }
        catch { return new List<SaveDto>(); }
    }

    private static void Log(string m) => System.Diagnostics.Debug.WriteLine(m);
}

public class SaveDto
{
    public string PlayerName { get; set; } = "";
    public int Will { get; set; }
    public int Faith { get; set; }
    public int Theories { get; set; }
    public string CurrentNode { get; set; } = "";
}

public class SessionDto
{
    public string Token { get; set; } = "";
}