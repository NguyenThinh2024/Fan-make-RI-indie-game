using UnityEngine;

namespace Game.GU.EffectSystem
{
    /// <summary>
    /// Interface cho object có thể nhận damage
    /// </summary>
    public interface IDamageReceiver
    {
        void TakeDamage(float damage);
    }

    /// <summary>
    /// Interface cho object có thể nhận shield
    /// </summary>
    public interface IShieldReceiver
    {
        void AddShield(float amount, float absorbRate, bool affectAllies);
    }

    /// <summary>
    /// Interface cho object có thể nhận buff
    /// </summary>
    public interface IBuffReceiver
    {
        void AddBuff(string stat, float value);
    }

    /// <summary>
    /// Interface cho object có thể nhận scout effect
    /// </summary>
    public interface IScoutReceiver
    {
        void Scout(float range, float duration, bool detectStealth, string revealType);
    }

    /// <summary>
    /// Interface cho object có thể nhận storage effect
    /// </summary>
    public interface IStorageReceiver
    {
        void AddStorage(int slots, string[] deny);
    }
    // ============ IMPLEMENTATIONS ============

    /// <summary>
    /// Ví dụ implementation của IDamageReceiver
    /// </summary>
    public class SimpleHealth : MonoBehaviour, IDamageReceiver
    {
        public void TakeDamage(float damage, float currentHealth, float maxHealth)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Debug.Log($"{gameObject.name} đã chết!");
                // Xử lý cái gì đó khi chết
            }
        }

        public void Heal(float amount, float currentHealth, float maxHealth)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        }
    }

    /// <summary>
    /// Ví dụ implementation của IShieldReceiver
    /// </summary>
    public class SimpleShield : MonoBehaviour, IShieldReceiver
    {
        public float currentShield;
        public float absorbRate;

        public void AddShield(float amount, float absorbRate, bool affectAllies)
        {
            currentShield += amount;
            this.absorbRate = absorbRate;
            Debug.Log($"{gameObject.name} hiện có {currentShield} shield");
        }

        public void RemoveShield(float amount)
        {
            currentShield = Mathf.Max(0, currentShield - amount);
        }
    }

    /// <summary>
    /// Ví dụ implementation của IBuffReceiver
    /// </summary>
    public class SimpleBuff : MonoBehaviour, IBuffReceiver
    {
        private System.Collections.Generic.Dictionary<string, float> buffs = new();

        public void AddBuff(string stat, float value)
        {
            if (buffs.ContainsKey(stat))
            {
                buffs[stat] += value;
            }
            else
            {
                buffs[stat] = value;
            }
            Debug.Log($"{gameObject.name} nhận buff {stat} +{value}");
        }

        public float GetBuffValue(string stat)
        {
            return buffs.ContainsKey(stat) ? buffs[stat] : 0f;
        }
    }

    /// <summary>
    /// Ví dụ implementation của IScoutReceiver
    /// </summary>
    public class SimpleScout : MonoBehaviour, IScoutReceiver
    {
        public void Scout(float range, float duration, bool detectStealth, string revealType)
        {
            Debug.Log($"{gameObject.name} scouting trong radius {range}m trong {duration}s. Detect stealth: {detectStealth}");
            // Vẽ debug circle
            Debug.DrawRay(transform.position, Vector3.up * range, Color.cyan, duration);
        }
    }

    /// <summary>
    /// Ví dụ implementation của IStorageReceiver
    /// </summary>
    public class SimpleStorage : MonoBehaviour, IStorageReceiver
    {
        public int currentSlots = 0;
        public string[] deniedItems;

        public void AddStorage(int slots, string[] deny)
        {
            currentSlots += slots;
            deniedItems = deny;
            Debug.Log($"{gameObject.name} hiện có {currentSlots} storage slots. Denied: {string.Join(", ", deny ?? new string[0])}");
        }
    }
}
