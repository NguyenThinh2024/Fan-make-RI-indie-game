using UnityEngine;
using UnityEditor;
using System.IO;
using Game.GU.SOModel;
using Game.GU.JsonModel;
public class GuJsonImporter : EditorWindow {
    private TextAsset jsonFile;

    [MenuItem("Tools/GU/Import JSON")]
    public static void Open() {
        GetWindow(typeof(GuJsonImporter));
    }

    void OnGUI() {
        GUILayout.Label("Import GU JSON → ScriptableObjects", EditorStyles.boldLabel);

        jsonFile = (TextAsset)EditorGUILayout.ObjectField("JSON File:", jsonFile, typeof(TextAsset), false);

        if (GUILayout.Button("IMPORT")) {
            Import();
        }
    }
void Import() {
    if (jsonFile == null) return;

    string folder = "Assets/Resources/GU/";
    
    // ✓ Xóa folder cũ
    if (Directory.Exists(folder)) {
        FileUtil.DeleteFileOrDirectory(folder);
        FileUtil.DeleteFileOrDirectory(folder + ".meta");
    }
    
    Directory.CreateDirectory(folder);

    string json = jsonFile.text;
    var wrapper = JsonHelper.FromJson<GU_Info_Json>(json);

    foreach (var gu in wrapper) {
        GuData so = ScriptableObject.CreateInstance<GuData>();
            so.code = gu.code;
            so.displayName = gu.name;
            so.dao = gu.dao;
            so.tier = gu.tier;
            so.category = gu.category;

            // Feeding
            so.feedingCode = gu.feeding?.code;
            so.feedingTags = gu.feeding?.foodTags;
            so.feedingDescription = gu.feeding?.description;
            so.feedingAmount = gu.feeding?.amount ?? 0;
            so.hungerRestore = gu.feeding?.hungerRestore ?? 0;

            // Consumption
            so.consumptionType = gu.consumption?.type;
            so.consumptionAmount = gu.consumption?.amount ?? 0;

            // Text
            so.description = gu.description;
            so.drawbacks = gu.drawbacks;

            // Effects
            so.effect = gu.effect != null ? JsonUtility.FromJson<Game.GU.SOModel.Effect>(JsonUtility.ToJson(gu.effect)) : null;
            

            // Stats
            so.stats = gu.stats != null ? JsonUtility.FromJson<Game.GU.SOModel.Stats>(JsonUtility.ToJson(gu.stats)) : null;
        AssetDatabase.CreateAsset(so, folder + gu.code + ".asset");
    }

    AssetDatabase.SaveAssets();
    AssetDatabase.Refresh();

    Debug.Log("<color=green>Imported GU JSON → ScriptableObject OK!</color>");
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
