using UnityEngine;
using UnityEditor;
using System.IO;
using Recipe_SO_Model;
using Recipe_Json_Model;

public class Recipe_importer : EditorWindow
{
    private TextAsset jsonFile;

    [MenuItem("Tools/Recipe/Import Recipe from JSON")]
    public static void Open()
    {
        GetWindow(typeof(Recipe_importer));
    }

    void OnGUI()
    {
        GUILayout.Label("Import Recipe JSON -> ScriptableObject", EditorStyles.boldLabel);
        jsonFile = (TextAsset)EditorGUILayout.ObjectField("Recipe JSON File", jsonFile, typeof(TextAsset), false);

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

        Import(jsonFile.text, "Assets/Resources/Recipe/");
    }

    public static void Import(string json, string folder)
    {
        if (Directory.Exists(folder))
        {
            FileUtil.DeleteFileOrDirectory(folder);
            FileUtil.DeleteFileOrDirectory(folder + ".meta");
        }

        Directory.CreateDirectory(folder);

        var recipes = JsonHelper.FromJson<Recipe_json>(json);

        foreach (var recipe in recipes)
        {
            Recipe_SO so = ScriptableObject.CreateInstance<Recipe_SO>();
            so.name = recipe.code;
            so.code = recipe.code;
            so.displayName = recipe.name;
            so.description = recipe.description;

            // Convert ingredients array
            if (recipe.ingredients != null)
            {
                so.ingredients = new Recipe_SO_Model.RecipeIngredient[recipe.ingredients.Length];
                for (int i = 0; i < recipe.ingredients.Length; i++)
                {
                    so.ingredients[i] = new Recipe_SO_Model.RecipeIngredient
                    {
                        itemCode = recipe.ingredients[i].itemCode,
                        quantity = recipe.ingredients[i].quantity
                    };
                }
            }

            so.method = recipe.method;
            so.baseSuccessRate = recipe.baseSuccessRate;
            so.timeSec = recipe.timeSec;
            so.requiredSkill = recipe.requiredSkill;
            so.requiredLevel = recipe.requiredLevel;
            so.fuelCost = recipe.fuelCost;

            // Convert outputs array
            if (recipe.possibleOutputs != null)
            {
                so.possibleOutputs = new Recipe_SO_Model.RecipeOutput[recipe.possibleOutputs.Length];
                for (int i = 0; i < recipe.possibleOutputs.Length; i++)
                {
                    so.possibleOutputs[i] = new Recipe_SO_Model.RecipeOutput
                    {
                        itemCode = recipe.possibleOutputs[i].itemCode,
                        quantity = recipe.possibleOutputs[i].quantity,
                        chance = recipe.possibleOutputs[i].chance
                    };
                }
            }

            so.applicableModifiers = recipe.applicableModifiers;

            AssetDatabase.CreateAsset(so, folder + so.code + ".asset");
        }

        Debug.Log($"<color=green>Imported {recipes.Length} recipes from JSON!</color>");
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
