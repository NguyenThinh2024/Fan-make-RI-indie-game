using UnityEngine;
namespace Item_SO_Model
{
    [CreateAssetMenu(fileName = "Item_", menuName = "Item/Item_SO")]
    public class Item_SO : ScriptableObject
    {
        public string code;
        public string displayName;
        public Sprite icon;

        public string itemType; // currency, food_gu, resource, equipment, etc.
        public string[] tags;

        [TextArea] public string description;

        [Header("Effect")]
        public ItemEffect effect;

        [Header("Stack & Resource")]
        public bool stackable;
        public int maxStack;
        public bool usableAsResource;

        [Header("Craft")]
        public bool usableInCraft;
        public int craftWeight; // used for recipe calculations
    }

    [System.Serializable]
    public class ItemEffect
    {
        public string type; // restoreEnergy, restoreHealth, buff, etc.
        public float value;
        public float durationSec;
        public string[] affectStats; // which stats this item affects
    }
}
