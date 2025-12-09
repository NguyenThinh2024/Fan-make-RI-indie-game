using UnityEngine;
namespace Player_SO_Model
{
    [CreateAssetMenu(fileName = "Player_", menuName = "Player/Player_SO")]
    public class Player_SO : ScriptableObject
    {
        public string code;
        public string displayName;
        public int rank;

        [Header("Aptitude & Aperture")]
        public string stage;
        public AptitudeRef aptitude;
        public string apertureCode;

        [Header("Stats")]
        public PlayerStats stats;

        [Header("GU Life")]
        public GuLifeRef guLife;

        [Header("Resources")]
        public int primevalStones;

        [Header("Metadata")]
        public string createdAt;
        public string lastLogin;
    }

    [System.Serializable]
    public class AptitudeRef
    {
        public string code;
        public string name;
    }

    [System.Serializable]
    public class PlayerStats
    {
        public float hp;
        public float hp_res;
        public float strength;
        public float defense;
        public float resistance;
        public string debuff;
        public float speed;
        public float critical_Damage;
        public float critical;
        public float luck;
    }

    [System.Serializable]
    public class GuLifeRef
    {
        public string code;
    }
}
