using GU_Json_Model;
using UnityEngine;

namespace Enemy_Json_Model
{
    [System.Serializable]
    public class Enemy_json
    {
        public string code;
        public string name;
        public string type;

        public string [] tags;
        public int tier;
        public string description;

        public string[] skills;
        public string aiType;
        public float detectRange;
        public float attackRange;

        public EnemyStats stats;

        public Feeding feeding;

        public GuList[] guList;

        public LootTable[] drops;
    }

    [System.Serializable]
    public class EnemyStats
    {
        public float health;
        public float damage;
        public float defense;
        public float speed; 
        public float willpower;
        public float critChance;
    }

    [System.Serializable]
    public class Feeding
    {
        public string[] foodTags;
    }

    [System.Serializable]
    public class GuList
    {
        public string code;
        public string effectType;
        public string trigger;
    }

    [System.Serializable]
    public class LootTable
    {
        public string code;
        public float rate;
        public int min;
        public int max;
    }
}
