using UnityEngine;

[System.Serializable]
public class GU_Info_Json
{
    public string code;
    public string name;
    public string dao;
    public int tier;
    public string category;

    public Feeding feeding;
    public Consumption consumption;
    public string description;
    public string drawbacks;
    public Effect effect;
    public Stats stats;
}

[System.Serializable]
public class Feeding {
    public string code;
    public string[] foodTags;
    public string description;
    public int amount;
    public int hungerRestore;
}

[System.Serializable]
public class Consumption {
    public string type;
    public float amount;
}

[System.Serializable]
public class Effect {
    public DamageEffect damage;
    public ShieldEffect shield;
    public BuffEffect buff;
    public ScoutEffect scount;
    public StorageEffect storage;

    public float cooldownSec;
    public bool check_GU;
}

[System.Serializable]
public class DamageEffect {
    public string type;
    public float value;
    public float scalingRatio;
    public float cooldownSec;
    public string areaType;
}

[System.Serializable]
public class ShieldEffect {
    public float hp;
    public float absorbRate;
    public float absorptionFlat;
    public string type;
    public float scalingRatio;
    public bool affectAllies;
}

[System.Serializable]
public class BuffEffect {
    public string targetGu;
    public string stat;
    public float scalingRatio;
}

[System.Serializable]
public class ScoutEffect {
    public float scoutRange;
    public float revealDurationSec;
    public bool detectStealth;
    public string revealType;
}

[System.Serializable]
public class StorageEffect {
    public int slots;
    public string[] deny;

}


[System.Serializable]
public class Stats {
    public float durability;
    public float willpower;
    public float maxWillpower;
    public float hunger;
    public float hungerSpeed;
    public float critChance;
}
