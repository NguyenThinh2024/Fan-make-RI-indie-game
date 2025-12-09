using UnityEngine;
using UnityEditor;
using System.IO;
using Item_SO_Model;
using Item_Json_Model;

public class Item_importer : EditorWindow
{
    private TextAsset jsonFile;

    [MenuItem("Tools/Item/Import Item from JSON")]
    public static void Open()
    {
        GetWindow(typeof(Item_importer));
    }

    void OnGUI()
    {
        GUILayout.Label("Import Item JSON -> ScriptableObject", EditorStyles.boldLabel);
        jsonFile = (TextAsset)EditorGUILayout.ObjectField("Item JSON File", jsonFile, typeof(TextAsset), false);

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

        Import(jsonFile.text, "Assets/Resources/Item/");
    }

    public static void Import(string json, string folder)
    {
        if (Directory.Exists(folder))
        {
            FileUtil.DeleteFileOrDirectory(folder);
            FileUtil.DeleteFileOrDirectory(folder + ".meta");
        }

        Directory.CreateDirectory(folder);

        var items = JsonHelper.FromJson<Item_json>(json);

        foreach (var item in items)
        {
            Item_SO so = ScriptableObject.CreateInstance<Item_SO>();
            so.name = item.code;
            so.code = item.code;
            so.displayName = item.name;
            so.itemType = item.type;
            so.tags = item.tags;
            so.description = item.description;

            so.effect = item.effect != null
                ? JsonUtility.FromJson<Item_SO_Model.ItemEffect>(JsonUtility.ToJson(item.effect))
                : null;

            so.stackable = item.stackable;
            so.maxStack = item.maxStack;
            so.usableAsResource = item.usableAsResource;
            so.usableInCraft = item.usableInCraft;
            so.craftWeight = item.craftWeight;

            AssetDatabase.CreateAsset(so, folder + so.code + ".asset");
        }

        Debug.Log($"<color=green>Imported {items.Length} items from JSON!</color>");
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
