using UnityEngine;

namespace GUMaster_Json_Model
{
    [System.Serializable]
    public class GUMaster_json
    {
        public string code;
        public string name;
        public string description;

        public float baseHP;
        public float baseAttack;
        public float baseDefense;
        public float baseSpeed;

        public float hpScaling;
        public float attackScaling;
        public float defenseScaling;

        public string type;
        public float costToSummon;
        public int maxLevel;
    }
}
