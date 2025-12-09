using UnityEngine;
namespace Aptitude_SO_Model
{
    [CreateAssetMenu(fileName = "Aptitude_", menuName = "Aptitude/Aptitude_SO")]
    public class Aptitude_SO : ScriptableObject
    {
        public string code;
        public string displayName;

        [TextArea] public string description;

        [Header("Essence")]
        public float minEssence;
        public float maxEssence;
        public float regenPerHour;

        [Header("Bonus")]
        public float staminaRegenBonus;

        [Header("Classification")]
        public string tag; // normal, rare, epic, legendary, etc.
    }
}
