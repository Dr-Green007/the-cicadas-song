using System;
using System.Collections.Generic;
using System.Text;

namespace thecicadassong;

public class GameState
{
    public string PlayerName { get; set; } = "";
    public string CurrentSceneId { get; set; } = "prologue";

    public int Will { get; set; } = 10;// Воля
    public int Faith { get; set; } = 0;// Вера
    public int Theories { get; set; } = 0;// Теории

    public Dictionary<string, int> Trust { get; set; } = new()// Доверие
    {
        ["Maria"] = 0,
        ["Jessica"] = 0,
        ["Rosa"] = 0,
        ["George"] = 0,
        ["Shannon"] = 0,
        ["Genji"] = 0,
        ["Virgilia"] = 0,
        ["Ange"] = 0
    };


}
