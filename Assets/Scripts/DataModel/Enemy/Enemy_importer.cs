using UnityEngine;
using UnityEditor;
using System.IO;
using Enemy_SO_Model;
using Enemy_Json_Model;
using System.Net.Sockets;

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
            so.code = enemy.code;
            so.displayName = enemy.name;
            so.type = enemy.type;
            so.tier = enemy.tier;
            so.description = enemy.description;
            so.tags = enemy.tags;
            so.Skills = enemy.skills;

            so.feedingData = enemy.feeding != null
                ? JsonUtility.FromJson<Enemy_SO_Model.FeedingData>(JsonUtility.ToJson(enemy.feeding))
                : null;

            so.stats = enemy.stats != null
                ? JsonUtility.FromJson<Enemy_SO_Model.EnemyStats>(JsonUtility.ToJson(enemy.stats))
                : null;

            // Copy guList directly
            if (enemy.guList != null && enemy.guList.Length > 0)
            {
                so.gu = new Enemy_SO_Model.GuList[enemy.guList.Length];
                for (int i = 0; i < enemy.guList.Length; i++)
                {
                    so.gu[i] = new Enemy_SO_Model.GuList
                    {
                        code = enemy.guList[i].code,
                        effectType = enemy.guList[i].effectType,
                        trigger = enemy.guList[i].trigger
                    };
                }
            }
            else
            {
                so.gu = null;
            }

            // Copy drops directly
            if (enemy.drops != null && enemy.drops.Length > 0)
            {
                so.lootTables = new Enemy_SO_Model.LootTable[enemy.drops.Length];
                for (int i = 0; i < enemy.drops.Length; i++)
                {
                    so.lootTables[i] = new Enemy_SO_Model.LootTable
                    {
                        code = enemy.drops[i].code,
                        rate = enemy.drops[i].rate,
                        min = enemy.drops[i].min,
                        max = enemy.drops[i].max
                    };
                }
            }
            else
            {
                so.lootTables = null;
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
