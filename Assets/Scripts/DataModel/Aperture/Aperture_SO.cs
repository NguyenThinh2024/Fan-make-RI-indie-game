using UnityEngine;
namespace Aperture_SO_Model
{
    [CreateAssetMenu(fileName = "Aperture_", menuName = "Aperture/Aperture_SO")]
    public class Aperture_SO : ScriptableObject
    {
        public string code;
        public string playerId;

        [Header("Aperture Wall")]
        public ApertureWall apertureWall;

        [Header("Primeval Essence")]
        public PrimevalEssence primevalEssence;

        [Header("GU List")]
        public GuItem[] guList;

        [Header("Metadata")]
        public string stage; // Initial, Early, Mid, Late
        public string updatedAt;
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
