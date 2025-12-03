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
        
    }
}