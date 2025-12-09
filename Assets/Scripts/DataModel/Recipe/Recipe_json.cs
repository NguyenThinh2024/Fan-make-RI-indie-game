using UnityEngine;

namespace Recipe_Json_Model
{
    [System.Serializable]
    public class Recipe_json
    {
        public string code;
        public string name;
        public string description;

        public RecipeIngredient[] ingredients;
        public string method;
        public float baseSuccessRate;
        public float timeSec;
        public string requiredSkill;
        public int requiredLevel;
        public float fuelCost;

        public RecipeOutput[] possibleOutputs;
        public string[] applicableModifiers;
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
        public float chance;
    }
}
