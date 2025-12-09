using UnityEngine;
using System.Collections.Generic;
using GU_SO_Model;
using Item_SO_Model;
using Recipe_SO_Model;
using Enemy_SO_Model;
using GUMaster_SO_Model;
using Aptitude_SO_Model;
using Aperture_SO_Model;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }

    [SerializeField] private GU_SO[] guDatabase;
    [SerializeField] private Item_SO[] itemDatabase;
    [SerializeField] private Recipe_SO[] recipeDatabase;
    [SerializeField] private Enemy_SO[] enemyDatabase;
    [SerializeField] private GUMaster_SO[] guMasterDatabase;
    [SerializeField] private Aptitude_SO[] aptitudeDatabase;
    [SerializeField] private Aperture_SO[] apertureDatabase;

    private Dictionary<string, GU_SO> guLookup = new();
    private Dictionary<string, Item_SO> itemLookup = new();
    private Dictionary<string, Recipe_SO> recipeLookup = new();
    private Dictionary<string, Enemy_SO> enemyLookup = new();
    private Dictionary<string, GUMaster_SO> guMasterLookup = new();
    private Dictionary<string, Aptitude_SO> aptitudeLookup = new();
    private Dictionary<string, Aperture_SO> apertureLookup = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeLookups();
    }

    private void InitializeLookups()
    {
        // Load from Resources if not assigned in inspector
        if (guDatabase == null || guDatabase.Length == 0)
        {
            guDatabase = Resources.LoadAll<GU_SO>("GU");
        }

        if (itemDatabase == null || itemDatabase.Length == 0)
        {
            itemDatabase = Resources.LoadAll<Item_SO>("Item");
        }

        if (recipeDatabase == null || recipeDatabase.Length == 0)
        {
            recipeDatabase = Resources.LoadAll<Recipe_SO>("Recipe");
        }

        if (enemyDatabase == null || enemyDatabase.Length == 0)
        {
            enemyDatabase = Resources.LoadAll<Enemy_SO>("Enemy");
        }

        if (guMasterDatabase == null || guMasterDatabase.Length == 0)
        {
            guMasterDatabase = Resources.LoadAll<GUMaster_SO>("GUMaster");
        }

        if (aptitudeDatabase == null || aptitudeDatabase.Length == 0)
        {
            aptitudeDatabase = Resources.LoadAll<Aptitude_SO>("Aptitude");
        }

        if (apertureDatabase == null || apertureDatabase.Length == 0)
        {
            apertureDatabase = Resources.LoadAll<Aperture_SO>("Aperture");
        }

        // Build lookups
        BuildLookup(guDatabase, guLookup, g => g.code);
        BuildLookup(itemDatabase, itemLookup, i => i.code);
        BuildLookup(recipeDatabase, recipeLookup, r => r.code);
        BuildLookup(enemyDatabase, enemyLookup, e => e.code);
        BuildLookup(guMasterDatabase, guMasterLookup, g => g.code);
        BuildLookup(aptitudeDatabase, aptitudeLookup, a => a.code);
        BuildLookup(apertureDatabase, apertureLookup, ap => ap.code);

        Debug.Log($"<color=cyan>DatabaseManager initialized:</color>");
        Debug.Log($"  GU: {guDatabase.Length}");
        Debug.Log($"  Items: {itemDatabase.Length}");
        Debug.Log($"  Recipes: {recipeDatabase.Length}");
        Debug.Log($"  Enemies: {enemyDatabase.Length}");
        Debug.Log($"  GUMasters: {guMasterDatabase.Length}");
        Debug.Log($"  Aptitudes: {aptitudeDatabase.Length}");
        Debug.Log($"  Apertures: {apertureDatabase.Length}");
    }

    private void BuildLookup<T>(T[] items, Dictionary<string, T> lookup, System.Func<T, string> keySelector)
    {
        lookup.Clear();
        foreach (var item in items)
        {
            string key = keySelector(item);
            if (!lookup.ContainsKey(key))
            {
                lookup[key] = item;
            }
            else
            {
                Debug.LogWarning($"Duplicate key: {key}");
            }
        }
    }

    // GU Database Access
    public GU_SO GetGU(string code)
    {
        if (guLookup.TryGetValue(code, out var gu))
            return gu;
        Debug.LogWarning($"GU not found: {code}");
        return null;
    }

    public GU_SO[] GetAllGU() => guDatabase;
    public GU_SO[] GetGUByCategory(string category)
    {
        var result = new List<GU_SO>();
        foreach (var gu in guDatabase)
        {
            if (gu.category == category)
                result.Add(gu);
        }
        return result.ToArray();
    }

    // Item Database Access
    public Item_SO GetItem(string code)
    {
        if (itemLookup.TryGetValue(code, out var item))
            return item;
        Debug.LogWarning($"Item not found: {code}");
        return null;
    }

    public Item_SO[] GetAllItems() => itemDatabase;
    public Item_SO[] GetItemsByType(string type)
    {
        var result = new List<Item_SO>();
        foreach (var item in itemDatabase)
        {
            if (item.itemType == type)
                result.Add(item);
        }
        return result.ToArray();
    }

    // Recipe Database Access
    public Recipe_SO GetRecipe(string code)
    {
        if (recipeLookup.TryGetValue(code, out var recipe))
            return recipe;
        Debug.LogWarning($"Recipe not found: {code}");
        return null;
    }

    public Recipe_SO[] GetAllRecipes() => recipeDatabase;

    // Enemy Database Access
    public Enemy_SO GetEnemy(string code)
    {
        if (enemyLookup.TryGetValue(code, out var enemy))
            return enemy;
        Debug.LogWarning($"Enemy not found: {code}");
        return null;
    }

    public Enemy_SO[] GetAllEnemies() => enemyDatabase;
    public Enemy_SO[] GetEnemiesByAI(string aiType)
    {
        var result = new List<Enemy_SO>();
        foreach (var enemy in enemyDatabase)
        {
            if (enemy.aiType == aiType)
                result.Add(enemy);
        }
        return result.ToArray();
    }

    // Utility Methods
    public bool HasGU(string code) => guLookup.ContainsKey(code);
    public bool HasItem(string code) => itemLookup.ContainsKey(code);
    public bool HasRecipe(string code) => recipeLookup.ContainsKey(code);
    public bool HasEnemy(string code) => enemyLookup.ContainsKey(code);

    // Get database statistics
    public void PrintDatabaseStats()
    {
        Debug.Log("<color=yellow>===== Database Statistics =====</color>");
        Debug.Log($"GU Count: {guDatabase.Length}");
        Debug.Log($"Item Count: {itemDatabase.Length}");
        Debug.Log($"Recipe Count: {recipeDatabase.Length}");
        Debug.Log($"Enemy Count: {enemyDatabase.Length}");
        Debug.Log($"GUMaster Count: {guMasterDatabase.Length}");
        Debug.Log($"Aptitude Count: {aptitudeDatabase.Length}");
        Debug.Log($"Aperture Count: {apertureDatabase.Length}");
        Debug.Log($"Total Objects: {guDatabase.Length + itemDatabase.Length + recipeDatabase.Length + enemyDatabase.Length + guMasterDatabase.Length + aptitudeDatabase.Length + apertureDatabase.Length}");
    }

    // GUMaster Database Access
    public GUMaster_SO GetGUMaster(string code)
    {
        if (guMasterLookup.TryGetValue(code, out var guMaster))
            return guMaster;
        Debug.LogWarning($"GUMaster not found: {code}");
        return null;
    }

    public GUMaster_SO[] GetAllGUMasters() => guMasterDatabase;
    public bool HasGUMaster(string code) => guMasterLookup.ContainsKey(code);

    // Aptitude Database Access
    public Aptitude_SO GetAptitude(string code)
    {
        if (aptitudeLookup.TryGetValue(code, out var aptitude))
            return aptitude;
        Debug.LogWarning($"Aptitude not found: {code}");
        return null;
    }

    public Aptitude_SO[] GetAllAptitudes() => aptitudeDatabase;
    public Aptitude_SO[] GetAptitudesByTag(string tag)
    {
        var result = new List<Aptitude_SO>();
        foreach (var aptitude in aptitudeDatabase)
        {
            if (aptitude.tag == tag)
                result.Add(aptitude);
        }
        return result.ToArray();
    }

    public bool HasAptitude(string code) => aptitudeLookup.ContainsKey(code);

    // Aperture Database Access
    public Aperture_SO GetAperture(string code)
    {
        if (apertureLookup.TryGetValue(code, out var aperture))
            return aperture;
        Debug.LogWarning($"Aperture not found: {code}");
        return null;
    }

    public Aperture_SO GetApertureByPlayerId(string playerId)
    {
        foreach (var aperture in apertureDatabase)
        {
            if (aperture.playerId == playerId)
                return aperture;
        }
        Debug.LogWarning($"Aperture for player not found: {playerId}");
        return null;
    }

    public Aperture_SO[] GetAllApertures() => apertureDatabase;
    public bool HasAperture(string code) => apertureLookup.ContainsKey(code);
}
