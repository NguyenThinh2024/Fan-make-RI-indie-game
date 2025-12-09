using UnityEngine;
using UnityEditor;
using System.IO;
using Aptitude_SO_Model;
using Aptitude_Json_Model;

public class Aptitude_importer : EditorWindow
{
    private TextAsset jsonFile;

    [MenuItem("Tools/Aptitude/Import Aptitude from JSON")]
    public static void Open()
    {
        GetWindow(typeof(Aptitude_importer));
    }

    void OnGUI()
    {
        GUILayout.Label("Import Aptitude JSON -> ScriptableObject", EditorStyles.boldLabel);
        jsonFile = (TextAsset)EditorGUILayout.ObjectField("Aptitude JSON File", jsonFile, typeof(TextAsset), false);

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

        Import(jsonFile.text, "Assets/Resources/Aptitude/");
    }

    public static void Import(string json, string folder)
    {
        if (Directory.Exists(folder))
        {
            FileUtil.DeleteFileOrDirectory(folder);
            FileUtil.DeleteFileOrDirectory(folder + ".meta");
        }

        Directory.CreateDirectory(folder);

        var items = JsonHelper.FromJson<Aptitude_json>(json);

        foreach (var item in items)
        {
            Aptitude_SO so = ScriptableObject.CreateInstance<Aptitude_SO>();
            so.name = item.code;
            so.code = item.code;
            so.displayName = item.name;
            so.description = item.description;

            so.minEssence = item.minEssence;
            so.maxEssence = item.maxEssence;
            so.regenPerHour = item.regenPerHour;
            so.staminaRegenBonus = item.staminaRegenBonus;

            so.tag = item.tag;

            AssetDatabase.CreateAsset(so, folder + so.code + ".asset");
        }

        Debug.Log($"<color=green>Imported {items.Length} Aptitudes from JSON!</color>");
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
