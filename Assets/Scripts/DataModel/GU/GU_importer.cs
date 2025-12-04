using UnityEngine;
using UnityEditor;
using System.IO;
using GU_SO_Model;
using GU_Json_Model;

public class GU_importer : EditorWindow
{
    private TextAsset jsonFile;
    [MenuItem("Tools/GU/Import GU from JSON")]

    public static void Open()
    {
        GetWindow(typeof(GU_importer));
    }

    void OnGUI()
    {
        GUILayout.Label("Import GU JSON -> ScriptableObject", EditorStyles.boldLabel);

        jsonFile = (TextAsset)EditorGUILayout.ObjectField("GU JSON File", jsonFile, typeof(TextAsset), false);

        if (GUILayout.Button("Import"))
        {
            Import();
        }
    }

    void Import()
    {
        if (jsonFile == null)
        {
            return;
        }
        string folder = "Assets/Resources/GU/";

        if(Directory.Exists(folder))
        {
            FileUtil.DeleteFileOrDirectory(folder);
            FileUtil.DeleteFileOrDirectory(folder + ".meta");
        }

        Directory.CreateDirectory(folder);

        string json = jsonFile.text;
        var wrapper = JsonHelper.FromJson<GU_json>(json);

        foreach (var gu in wrapper)
        {
            GU_SO so = ScriptableObject.CreateInstance<GU_SO>();
                so.code = gu.code;
                so.name = gu.name;
                so.dao = gu.dao;
                so.tier = gu.tier;
                so.category = gu.category;

                so.feedingCode = gu.feeding?.code;
                so.feedingTags = gu.feeding?.tags;
                so.feedingDescription = gu.feeding?.description;
                so.feedingAmount = gu.feeding?.amount ?? 0;
                so.hungerRestore = gu.feeding?.hungerRestore ?? 0;

                so.consumptionType = gu.consumption?.type;
                so.consumptionAmount = gu.consumption?.amount ?? 0;

                so.description = gu.description;
                so.drawbacks = gu.drawbacks;

                so.effect = gu.effect != null ? JsonUtility.FromJson<GU_SO_Model.Effect>(JsonUtility.ToJson(gu.effect)) : null;

                so.stats = gu.stats != null ? JsonUtility.FromJson<GU_SO_Model.Stats>(JsonUtility.ToJson(gu.stats)) : null;
            AssetDatabase.CreateAsset(so, folder + so.code + ".asset");    
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("<color=green>Imported GU JSON -> ScriptableObject OK!</color>");
        
    }

    public static class JsonHelper {
    public static T[] FromJson<T>(string json) {
        string wrapped = "{\"Items\":" + json + "}";
        Wrapper<T> w = JsonUtility.FromJson<Wrapper<T>>(wrapped);
        return w.Items;
    }

    [System.Serializable]
    private class Wrapper<T> {
        public T[] Items;
    }
}

}