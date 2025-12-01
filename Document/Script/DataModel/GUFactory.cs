using UnityEngine;
using System.Collections.Generic;
using Game.GU.SOModel; 
using Game.GU.JsonModel;

public static class GUFactory {
    private static Dictionary<string, GuData> cache;

    private static void LoadAll() {
        if (cache != null) return;

        cache = new Dictionary<string, GuData>();
        var all = Resources.LoadAll<GuData>("GU");

        foreach (var gu in all)
            cache[gu.code] = gu;
    }

    public static GuData Get(string code) {
        LoadAll();

        if (!cache.ContainsKey(code)) {
            Debug.LogError("GU code not found: " + code);
            return null;
        }

        return cache[code];
    }
}
