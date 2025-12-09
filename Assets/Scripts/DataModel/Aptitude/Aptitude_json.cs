using UnityEngine;

namespace Aptitude_Json_Model
{
    [System.Serializable]
    public class Aptitude_json
    {
        public string code;
        public string name;
        public string description;

        public float minEssence;
        public float maxEssence;
        public float regenPerHour;
        public float staminaRegenBonus;

        public string tag;
    }
}
