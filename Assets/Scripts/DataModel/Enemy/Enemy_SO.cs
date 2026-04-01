using System.Runtime.ExceptionServices;
using Enemy_Json_Model;
using UnityEngine;
namespace Enemy_SO_Model
{
    [CreateAssetMenu(fileName = "Enemy_", menuName = "Enemy/Enemy_SO")]
    public class Enemy_SO : ScriptableObject
    {
        public string code;
        public string displayName;
        public string type; 
        public int tier;
        public string[] tags;
        public Sprite icon;
        [TextArea] public string description;

        [Header("Feeding")]
        public FeedingData feedingData;

        [Header("Stats")]
        public EnemyStats stats;

        [Header("Gu List")]
        public GuList[] gu;

        [Header("Loot Table")]
        public LootTable[] lootTables;

        [Header("Skills")]
        public string[] Skills;

        [Header("AI")]
        public string aiType; // melee, ranged, mage, hybrid, etc.
        public float detectRange;
        public float attackRange;
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
    public class FeedingData
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
