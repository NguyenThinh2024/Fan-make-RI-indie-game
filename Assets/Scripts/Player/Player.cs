using UnityEngine;
using Player_Json_Model;
using Aptitude_SO_Model;
using GU_SO_Model;
using System.IO;

namespace Player_Model
{
    public class PlayerData : MonoBehaviour
    {
        private const string JSON_PATH = "Assets/Database/GU_master.json";

        [Header("Load Settings")]
        [Tooltip("Player ID to load from GU_master.json")]
        public string targetPlayerId = "player_001";
        public bool loadOnStart = true;

        [Header("Player Info")]
        public string playerId;
        public string playerName;
        public int rank;
        public string stage;

        [Header("References (loaded via code)")]
        public Aptitude_SO aptitude;
        public GU_SO guLife;
        public string apertureId;

        [Header("Stats")]
        public float health;
        public float currentHealth;
        public float hp_res;
        public float strength;
        public float defense;
        public float resistance;
        public string debuff;
        public float speed;
        public float critical_Damage;
        public float critical;
        public float luck;

        [Header("Resources")]
        public float primevalStones;
        public string[] inventory;
        public string[] killerMoveIds;

        [Header("Transform Data")]
        public Vector3 savedPosition;
        public float savedRotation;

        [Header("Timestamps")]
        public string created_at;
        public string last_login;
        private Player_json _rawData;

        private void Start()
        {
            if (loadOnStart && !string.IsNullOrEmpty(targetPlayerId))
            {
                LoadFromJson(targetPlayerId);
            }
        }
        [ContextMenu("Load Player Data")]
        public void LoadPlayerDataFromInspector()
        {
            LoadFromJson(targetPlayerId);
            Debug.Log($"Loaded player: {playerName} (ID: {playerId})");
        }

        public void LoadFromJson(string targetPlayerId)
        {
            string fullPath = Path.Combine(Application.dataPath, "..", JSON_PATH);
            
            if (!File.Exists(fullPath))
            {
                Debug.LogError($"Cannot find GU_master.json at: {fullPath}");
                return;
            }

            string jsonContent = File.ReadAllText(fullPath);
            Player_json[] players = JsonHelper.FromJson<Player_json>(jsonContent);
            
            foreach (var player in players)
            {
                if (player._id == targetPlayerId)
                {
                    ApplyData(player);
                    return;
                }
            }

            Debug.LogWarning($"Player with ID '{targetPlayerId}' not found in GU_master.json");
        }
        public void ApplyData(Player_json data)
        {
            _rawData = data;
            playerId = data._id;
            playerName = data.name;
            rank = data.rank;
            stage = data.stage;

            apertureId = data.aperture;

            if (data.stat != null)
            {
                health = data.stat.health;
                currentHealth = data.stat.health;
                hp_res = data.stat.hp_res;
                strength = data.stat.strength;
                defense = data.stat.defense;
                resistance = data.stat.resistance;
                debuff = data.stat.debuff;
                speed = data.stat.speed;
                critical_Damage = data.stat.critical_Damage;
                critical = data.stat.critical;
                luck = data.stat.luck;
            }

            primevalStones = data.primevalStones;
            inventory = data.inventory ?? new string[0];

            if (data.killerMove_List != null)
            {
                killerMoveIds = new string[data.killerMove_List.Length];
                for (int i = 0; i < data.killerMove_List.Length; i++)
                {
                    killerMoveIds[i] = data.killerMove_List[i].killerMove_id;
                }
            }

            // Position
            if (data.position != null)
            {
                savedPosition = new Vector3(data.position.x, data.position.y, data.position.z);
            }
            savedRotation = data.rotation;

            // Timestamps
            created_at = data.created_at.ToString();
            last_login = data.last_login.ToString();

            // Load ScriptableObject references
            LoadScriptableObjectReferences(data);
        }
        private void LoadScriptableObjectReferences(Player_json data)
        {
            if (data.aptitude != null && !string.IsNullOrEmpty(data.aptitude.code))
            {
                Aptitude_SO[] allAptitudes = Resources.LoadAll<Aptitude_SO>("Aptitude");
                foreach (var apt in allAptitudes)
                {
                    if (apt.code == data.aptitude.code)
                    {
                        aptitude = apt;
                        break;
                    }
                }
            }

            if (data.guLife != null && !string.IsNullOrEmpty(data.guLife.code))
            {
                GU_SO[] allGUs = Resources.LoadAll<GU_SO>("GU");
                foreach (var gu in allGUs)
                {
                    if (gu.code == data.guLife.code)
                    {
                        guLife = gu;
                        break;
                    }
                }
            }
        }
        public void ApplyTransform()
        {
            transform.position = savedPosition;
            transform.rotation = Quaternion.Euler(0, savedRotation, 0);
        }
        public Player_json GetRawData() => _rawData;

        public void ChangeHealth(float amount)
        {
            currentHealth += amount;

            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
        
        }
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            string wrappedJson = "{\"items\":" + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(wrappedJson);
            return wrapper.items;
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] items;
        }
    }


}
