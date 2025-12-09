using UnityEngine;
using UnityEditor;
using System.IO;
using Enemy_SO_Model;
using Enemy_Json_Model;

public class Enemy_importer : EditorWindow
{
    private TextAsset jsonFile;

    [MenuItem("Tools/Enemy/Import Enemy from JSON")]
    public static void Open()
    {
        GetWindow(typeof(Enemy_importer));
    }

    void OnGUI()
    {
        GUILayout.Label("Import Enemy JSON -> ScriptableObject", EditorStyles.boldLabel);
        jsonFile = (TextAsset)EditorGUILayout.ObjectField("Enemy JSON File", jsonFile, typeof(TextAsset), false);

        if (GUILayout.Button("Import", GUILayout.Height(40)))
        {
            Import();
        }
    }

    void Import()
    {
        if (jsonFile == null)
        {
            EditorUtility.DisplayDialog("Error", "Please select a JSON file", "OK");
            return;
        }

        Import(jsonFile.text, "Assets/Resources/Enemy/");
    }

    public static void Import(string json, string folder)
    {
        if (Directory.Exists(folder))
        {
            FileUtil.DeleteFileOrDirectory(folder);
            FileUtil.DeleteFileOrDirectory(folder + ".meta");
        }

        Directory.CreateDirectory(folder);

        var enemies = JsonHelper.FromJson<Enemy_json>(json);

        foreach (var enemy in enemies)
        {
            Enemy_SO so = ScriptableObject.CreateInstance<Enemy_SO>();
            so.name = enemy.code;
            so.code = enemy.code;
            so.displayName = enemy.name;
            so.description = enemy.description;

            so.stats = enemy.stats != null
                ? JsonUtility.FromJson<Enemy_SO_Model.EnemyStats>(JsonUtility.ToJson(enemy.stats))
                : null;

            so.aperture = enemy.aperture != null
                ? JsonUtility.FromJson<Enemy_SO_Model.ApertureData>(JsonUtility.ToJson(enemy.aperture))
                : null;

            // Convert loot tables
            if (enemy.lootTables != null)
            {
                so.lootTables = new Enemy_SO_Model.LootTable[enemy.lootTables.Length];
                for (int i = 0; i < enemy.lootTables.Length; i++)
                {
                    so.lootTables[i] = new Enemy_SO_Model.LootTable
                    {
                        itemCode = enemy.lootTables[i].itemCode,
                        minQuantity = enemy.lootTables[i].minQuantity,
                        maxQuantity = enemy.lootTables[i].maxQuantity,
                        chance = enemy.lootTables[i].chance
                    };
                }
            }

            so.aiType = enemy.aiType;
            so.detectRange = enemy.detectRange;
            so.attackRange = enemy.attackRange;

            AssetDatabase.CreateAsset(so, folder + so.code + ".asset");
        }

        Debug.Log($"<color=green>Imported {enemies.Length} enemies from JSON!</color>");
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            string wrapped = "{\"Items\":" + json + "}";
            Wrapper<T> w = JsonUtility.FromJson<Wrapper<T>>(wrapped);
            return w.Items;
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}
