using UnityEngine;
namespace GU_SO_Model
{
    [CreateAssetMenu(fileName = "GU_", menuName = "GU/GU_SO")]
    public class GU_SO : ScriptableObject
    {
        public string code;
        public string displayName;
        public string dao;
        public int tier;
        public string category;

        [Header("Feeding")]
        public string feedingCode;
        public string[] feedingTags;
        public string feedingDescription;
        public int feedingAmount;
        public int hungerRestore;

        [Header("Consumption")]
        public string consumptionType;
        public float consumptionAmount;

        [TextArea] public string description;
        [TextArea] public string drawbacks;

        public Effect effect;
        public Stats stats;
    }

    [System.Serializable]
    public class Effect
    {
        public DamageEffect damage;
        public ShieldEffect shield;
        public BuffEffect buff;
        public ScoutEffect scout;
        public StorageEffect storage;

        public float cooldownSec;
        public bool check_GU;
    }

    [System.Serializable]
    public class DamageEffect
    {
        public string type;
        public float value;
        public float scalingRatio;
        public float cooldownSec;
        public string areaType;
    }
    [System.Serializable]
    public class ShieldEffect
    {
        public float hp;
        public float absorbRate;
        public float absorptionFlat;
        public string type;
        public float scalingRatio;
        public bool affectAllies;
    }
    [System.Serializable]
    public class BuffEffect
    {
        public string targetGu;
        public string stat;
        public float scalingRatio;
    }
    [System.Serializable]
    public class ScoutEffect
    {
        public float scoutRange;
        public float revealDurationSec;
        public bool detectStealth;
        public string revealType;
    }

    [System.Serializable]
    public class StorageEffect
    {
        public int slots;
        public string[] deny;
    }

    [System.Serializable]
    public class Stats
    {
        public float durability;
        public float willpower;
        public float maxWillpower;
        public float hunger;
        public float hungerSpeed;
        public float critChance;
    }
}
