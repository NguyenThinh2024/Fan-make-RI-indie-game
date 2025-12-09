using UnityEngine;

namespace Item_Json_Model
{
    [System.Serializable]
    public class Item_json
    {
        public string code;
        public string name;
        public string type;
        public string[] tags;
        public string description;

        public ItemEffect effect;
        public bool stackable;
        public int maxStack;
        public bool usableAsResource;
        public bool usableInCraft;
        public int craftWeight;
    }

    [System.Serializable]
    public class ItemEffect
    {
        public string type;
        public float value;
        public float durationSec;
        public string[] affectStats;
    }
}
