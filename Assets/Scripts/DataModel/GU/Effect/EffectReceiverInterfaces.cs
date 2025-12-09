using UnityEngine;
namespace Gu_Effect_Systems
{
    public interface IDamageReceiver
    {
        void TakeDamage(float damage);
    }

    public interface IShieldReceiver
    {
        void AddShield(float amount, float absorbRate, bool affectAllies);
    }

    public interface IBuffReceiver
    {
        void AddBuff(string stat, float amount, float durationSec);
    }

    public interface IScoutReceiver
    {
        void Scout(float range, float duration, bool detectStealth, string revealType);
    }

    public interface IStorageReceiver
    {
        void AddStorage(int slots, string[] deny);
    }

    public class SimpleHealth : MonoBehaviour, IDamageReceiver
    {
        public float currentHealth = 100f;
        public float maxHealth = 100f;
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Debug.Log($"{gameObject.name} has been destroyed!");
                Destroy(gameObject);
            }
        }

        public void Heal(float amount)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        }
    }
}
