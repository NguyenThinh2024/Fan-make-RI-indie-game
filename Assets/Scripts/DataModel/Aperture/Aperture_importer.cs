using UnityEngine;
using UnityEditor;
using System.IO;
using Aperture_SO_Model;
using Aperture_Json_Model;

public class Aperture_importer : EditorWindow
{
    private TextAsset jsonFile;

    [MenuItem("Tools/Aperture/Import Aperture from JSON")]
    public static void Open()
    {
        GetWindow(typeof(Aperture_importer));
    }

    void OnGUI()
    {
        GUILayout.Label("Import Aperture JSON -> ScriptableObject", EditorStyles.boldLabel);
        jsonFile = (TextAsset)EditorGUILayout.ObjectField("Aperture JSON File", jsonFile, typeof(TextAsset), false);

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

        Import(jsonFile.text, "Assets/Resources/Aperture/");
    }

    public static void Import(string json, string folder)
    {
        if (Directory.Exists(folder))
        {
            FileUtil.DeleteFileOrDirectory(folder);
            FileUtil.DeleteFileOrDirectory(folder + ".meta");
        }

        Directory.CreateDirectory(folder);

        var items = JsonHelper.FromJson<Aperture_json>(json);

        foreach (var item in items)
        {
            Aperture_SO so = ScriptableObject.CreateInstance<Aperture_SO>();
            so.name = item._id;
            so.code = item._id;
            so.playerId = item.player_id;
            so.stage = item.stage;

            so.apertureWall = item.aperture_wall != null
                ? JsonUtility.FromJson<Aperture_SO_Model.ApertureWall>(JsonUtility.ToJson(item.aperture_wall))
                : null;

            so.primevalEssence = item.primevalEssence != null
                ? JsonUtility.FromJson<Aperture_SO_Model.PrimevalEssence>(JsonUtility.ToJson(item.primevalEssence))
                : null;

            // Convert GuItem array
            if (item.guList != null)
            {
                so.guList = new Aperture_SO_Model.GuItem[item.guList.Length];
                for (int i = 0; i < item.guList.Length; i++)
                {
                    so.guList[i] = new Aperture_SO_Model.GuItem
                    {
                        order = item.guList[i].order,
                        code = item.guList[i].code,
                        usageCount = item.guList[i].usageCount,
                        bonusCrit = item.guList[i].bonusCrit
                    };
                }
            }

            so.updatedAt = item.updated_at;

            AssetDatabase.CreateAsset(so, folder + so.code + ".asset");
        }

        Debug.Log($"<color=green>Imported {items.Length} Apertures from JSON!</color>");
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
