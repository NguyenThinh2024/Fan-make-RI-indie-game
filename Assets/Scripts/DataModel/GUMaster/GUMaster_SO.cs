using UnityEngine;
namespace GUMaster_SO_Model
{
    [CreateAssetMenu(fileName = "GUMaster_", menuName = "GUMaster/GUMaster_SO")]
    public class GUMaster_SO : ScriptableObject
    {
        public string code;
        public string displayName;

        [TextArea] public string description;

        [Header("Master Stats")]
        public float baseHP;
        public float baseAttack;
        public float baseDefense;
        public float baseSpeed;

        [Header("Scaling")]
        public float hpScaling;
        public float attackScaling;
        public float defenseScaling;

        [Header("Special")]
        public string type; // physical, magical, support, etc.
        public float costToSummon;
        public int maxLevel;
    }
}
