using UnityEngine;
using UnityEditor;
using System.IO;
using GUMaster_SO_Model;
using GUMaster_Json_Model;

public class GUMaster_importer : EditorWindow
{
    private TextAsset jsonFile;

    [MenuItem("Tools/GUMaster/Import GUMaster from JSON")]
    public static void Open()
    {
        GetWindow(typeof(GUMaster_importer));
    }

    void OnGUI()
    {
        GUILayout.Label("Import GUMaster JSON -> ScriptableObject", EditorStyles.boldLabel);
        jsonFile = (TextAsset)EditorGUILayout.ObjectField("GUMaster JSON File", jsonFile, typeof(TextAsset), false);

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

        Import(jsonFile.text, "Assets/Resources/GUMaster/");
    }

    public static void Import(string json, string folder)
    {
        if (Directory.Exists(folder))
        {
            FileUtil.DeleteFileOrDirectory(folder);
            FileUtil.DeleteFileOrDirectory(folder + ".meta");
        }

        Directory.CreateDirectory(folder);

        var items = JsonHelper.FromJson<GUMaster_json>(json);

        foreach (var item in items)
        {
            GUMaster_SO so = ScriptableObject.CreateInstance<GUMaster_SO>();
            so.code = item.code;
            so.displayName = item.name;
            so.description = item.description;

            so.baseHP = item.baseHP;
            so.baseAttack = item.baseAttack;
            so.baseDefense = item.baseDefense;
            so.baseSpeed = item.baseSpeed;

            so.hpScaling = item.hpScaling;
            so.attackScaling = item.attackScaling;
            so.defenseScaling = item.defenseScaling;

            so.type = item.type;
            so.costToSummon = item.costToSummon;
            so.maxLevel = item.maxLevel;

            AssetDatabase.CreateAsset(so, folder + so.code + ".asset");
        }

        Debug.Log($"<color=green>Imported {items.Length} GUMasters from JSON!</color>");
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
