using UnityEngine;
namespace Recipe_SO_Model
{
    [CreateAssetMenu(fileName = "Recipe_", menuName = "Recipe/Recipe_SO")]
    public class Recipe_SO : ScriptableObject
    {
        public string code;
        public string displayName;

        [TextArea] public string description;

        [Header("Ingredients")]
        public RecipeIngredient[] ingredients;

        [Header("Crafting")]
        public string method; // grind, brew, combine, etc.
        public float baseSuccessRate;
        public float timeSec;
        public string requiredSkill;
        public int requiredLevel;
        public float fuelCost;

        [Header("Output")]
        public RecipeOutput[] possibleOutputs;

        [Header("Modifiers")]
        public string[] applicableModifiers; // support GU codes that can boost this recipe
    }

    [System.Serializable]
    public class RecipeIngredient
    {
        public string itemCode;
        public int quantity;
    }

    [System.Serializable]
    public class RecipeOutput
    {
        public string itemCode;
        public int quantity;
        public float chance; // 0..1, chance to get this output
    }
}
