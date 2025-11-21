using UnityEngine;
using System;

[Serializable]
public class Aperture
{
    public string _id;
    public string player_id;
    public string stage;
    public ApertureWall aperture_wall;
    public PrimevalEssence primevalEssence;
    public GuItem[] guList;
    public string updated_at;
}

[Serializable]
public class ApertureWall
{
    public string type;
    public int stability;
    public float recovery_Rate;
}

[Serializable]
public class PrimevalEssence
{
    public float primevalEssence_current;
    public float primevalEssence_max;
    public string primevalEssence_color;
}

[Serializable]
public class GuItem
{
    public int order;
    public string code;
    public int usageCount;
    public float bonusCrit;
}

public class EnemyDatabase {
    public Aperture[] apertures;
}