using UnityEngine;

namespace Aperture_Json_Model
{
    [System.Serializable]
    public class Aperture_json
    {
        public string _id;
        public string player_id;
        public string stage;

        public ApertureWall aperture_wall;
        public PrimevalEssence primevalEssence;
        public GuItem[] guList;

        public string updated_at;
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
}
