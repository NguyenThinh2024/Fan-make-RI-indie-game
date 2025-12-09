using UnityEngine;
using UnityEditor;
using System.IO;

public class BulkDatabaseImporter : EditorWindow
{
    private TextAsset guJsonFile;
    private TextAsset itemJsonFile;
    private TextAsset recipeJsonFile;
    private TextAsset enemyJsonFile;
    private TextAsset guMasterJsonFile;
    private TextAsset aptitudeJsonFile;
    private TextAsset apertureJsonFile;
    private bool autoRefresh = true;

    [MenuItem("Tools/Database/Bulk Import All Data")]
    public static void Open()
    {
        GetWindow(typeof(BulkDatabaseImporter));
    }

    private void OnGUI()
    {
        GUILayout.Label("Bulk Database Importer", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(
            "Import all JSON files to ScriptableObjects at once. " +
            "Each type will be saved to Assets/Resources/{Type}/",
            MessageType.Info
        );

        EditorGUILayout.Space();

        guJsonFile = (TextAsset)EditorGUILayout.ObjectField(
            "GU JSON File", guJsonFile, typeof(TextAsset), false);

        itemJsonFile = (TextAsset)EditorGUILayout.ObjectField(
            "Item JSON File", itemJsonFile, typeof(TextAsset), false);

        recipeJsonFile = (TextAsset)EditorGUILayout.ObjectField(
            "Recipe JSON File", recipeJsonFile, typeof(TextAsset), false);

        enemyJsonFile = (TextAsset)EditorGUILayout.ObjectField(
            "Enemy JSON File", enemyJsonFile, typeof(TextAsset), false);

        guMasterJsonFile = (TextAsset)EditorGUILayout.ObjectField(
            "GUMaster JSON File", guMasterJsonFile, typeof(TextAsset), false);

        aptitudeJsonFile = (TextAsset)EditorGUILayout.ObjectField(
            "Aptitude JSON File", aptitudeJsonFile, typeof(TextAsset), false);

        apertureJsonFile = (TextAsset)EditorGUILayout.ObjectField(
            "Aperture JSON File", apertureJsonFile, typeof(TextAsset), false);

        EditorGUILayout.Space();
        autoRefresh = EditorGUILayout.Toggle("Auto Refresh AssetDB", autoRefresh);

        EditorGUILayout.Space();
        GUILayout.Label("Import Status");

        EditorGUILayout.LabelField("Ready to import:", GetReadyCount().ToString());

        EditorGUILayout.Space();

        if (GUILayout.Button("Import All", GUILayout.Height(50)))
        {
            ImportAll();
        }

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox(
            "Tips:\n" +
            "1. Make sure JSON files are properly formatted\n" +
            "2. Each JSON file should be an array of objects\n" +
            "3. Code field is required for each object\n" +
            "4. Import may take a few seconds for large datasets",
            MessageType.Info
        );
    }

    private int GetReadyCount()
    {
        int count = 0;
        if (guJsonFile != null) count++;
        if (itemJsonFile != null) count++;
        if (recipeJsonFile != null) count++;
        if (enemyJsonFile != null) count++;
        if (guMasterJsonFile != null) count++;
        if (aptitudeJsonFile != null) count++;
        if (apertureJsonFile != null) count++;
        return count;
    }

    private void ImportAll()
    {
        int successCount = 0;
        int totalCount = 0;

        EditorUtility.DisplayProgressBar("Importing Database", "Starting import...", 0);

        try
        {
            if (guJsonFile != null)
            {
                totalCount++;
                EditorUtility.DisplayProgressBar("Importing Database", "Importing GU data...", 0.12f);
                ImportGU(guJsonFile);
                successCount++;
            }

            if (itemJsonFile != null)
            {
                totalCount++;
                EditorUtility.DisplayProgressBar("Importing Database", "Importing Item data...", 0.25f);
                ImportItem(itemJsonFile);
                successCount++;
            }

            if (recipeJsonFile != null)
            {
                totalCount++;
                EditorUtility.DisplayProgressBar("Importing Database", "Importing Recipe data...", 0.37f);
                ImportRecipe(recipeJsonFile);
                successCount++;
            }

            if (enemyJsonFile != null)
            {
                totalCount++;
                EditorUtility.DisplayProgressBar("Importing Database", "Importing Enemy data...", 0.50f);
                ImportEnemy(enemyJsonFile);
                successCount++;
            }

            if (guMasterJsonFile != null)
            {
                totalCount++;
                EditorUtility.DisplayProgressBar("Importing Database", "Importing GUMaster data...", 0.62f);
                ImportGUMaster(guMasterJsonFile);
                successCount++;
            }

            if (aptitudeJsonFile != null)
            {
                totalCount++;
                EditorUtility.DisplayProgressBar("Importing Database", "Importing Aptitude data...", 0.75f);
                ImportAptitude(aptitudeJsonFile);
                successCount++;
            }

            if (apertureJsonFile != null)
            {
                totalCount++;
                EditorUtility.DisplayProgressBar("Importing Database", "Importing Aperture data...", 0.87f);
                ImportAperture(apertureJsonFile);
                successCount++;
            }

            if (autoRefresh)
            {
                EditorUtility.DisplayProgressBar("Importing Database", "Refreshing assets...", 0.95f);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog(
                "Success",
                $"Successfully imported {successCount} out of {totalCount} database(s)!",
                "OK"
            );

            Debug.Log($"<color=green>Bulk import complete: {successCount}/{totalCount} successful</color>");
        }
        catch (System.Exception ex)
        {
            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("Error", $"Import failed:\n{ex.Message}", "OK");
            Debug.LogError($"<color=red>Bulk import error: {ex}</color>");
        }
    }

    private void ImportGU(TextAsset jsonFile)
    {
        GU_importer.Import(jsonFile.text, "Assets/Resources/GU/");
    }

    private void ImportItem(TextAsset jsonFile)
    {
        Item_importer.Import(jsonFile.text, "Assets/Resources/Item/");
    }

    private void ImportRecipe(TextAsset jsonFile)
    {
        Recipe_importer.Import(jsonFile.text, "Assets/Resources/Recipe/");
    }

    private void ImportEnemy(TextAsset jsonFile)
    {
        Enemy_importer.Import(jsonFile.text, "Assets/Resources/Enemy/");
    }

    private void ImportGUMaster(TextAsset jsonFile)
    {
        GUMaster_importer.Import(jsonFile.text, "Assets/Resources/GUMaster/");
    }

    private void ImportAptitude(TextAsset jsonFile)
    {
        Aptitude_importer.Import(jsonFile.text, "Assets/Resources/Aptitude/");
    }

    private void ImportAperture(TextAsset jsonFile)
    {
        Aperture_importer.Import(jsonFile.text, "Assets/Resources/Aperture/");
    }
}