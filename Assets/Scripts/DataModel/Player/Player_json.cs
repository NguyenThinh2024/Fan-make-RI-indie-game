using System;
using UnityEngine;

namespace Player_Json_Model
{
    [System.Serializable]
    public class Player_json
    {
        public string _id;
        public string name;
        public int rank;
        public string stage;
        public Aptitude aptitude;
        public string aperture;
        public PlayerStats stat;
        public GuLife guLife;
        public float primevalStones;
        public string[] inventory;
        public KillerMove[] killerMove_List;
        public Position position;
        public float rotation;
        public DateTime created_at;
        public DateTime last_login;
    }

    [System.Serializable]
    public class Aptitude
    {
        public string code;
        public string name;
    }

    [System.Serializable]
    public class PlayerStats 
    {
        public float health;
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
    public class GuLife
    {
        public string code;
    }

    [System.Serializable]
    public class KillerMove
    {
        public string killerMove_id;
    }

    [System.Serializable]
    public class Position
    {
        public float x;
        public float y;
        public float z;
    }
}

