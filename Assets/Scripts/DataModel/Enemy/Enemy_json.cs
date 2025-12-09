using UnityEngine;

namespace Enemy_Json_Model
{
    [System.Serializable]
    public class Enemy_json
    {
        public string code;
        public string name;
        public string description;

        public EnemyStats stats;
        public ApertureData aperture;
        public LootTable[] lootTables;

        public string aiType;
        public float detectRange;
        public float attackRange;
    }

    [System.Serializable]
    public class EnemyStats
    {
        public float health;
        public float attack;
        public float defense;
        public float speed;
        public float willpower;
        public float critChance;
    }

    [System.Serializable]
    public class ApertureData
    {
        public string stage;
        public ApertureWall aperture_wall;
        public PrimevalEssence primevalEssence;
        public GuItem[] guList;
    }

    [System.Serializable]
    public class ApertureWall
    {
        public string type;
        public float stability;
        public float recovery_Rate;
    }

    [System.Serializable]
    public class PrimevalEssence
    {
        public float primevalEssence_current;
        public float primevalEssence_max;
        public string primevalEssence_color;
    }

    [System.Serializable]
    public class GuItem
    {
        public int order;
        public string code;
        public int usageCount;
        public float bonusCrit;
    }

    [System.Serializable]
    public class LootTable
    {
        public string itemCode;
        public int minQuantity;
        public int maxQuantity;
        public float chance;
    }
}
