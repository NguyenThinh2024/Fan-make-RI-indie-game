using UnityEngine;
using Enemy_SO_Model;
using UnityEditor.Experimental.GraphView;

public class Enermy_health : MonoBehaviour
{
    [SerializeField] public Enemy_SO enermyData;
    private float maxHealth;
    public float currentHealth;
    private void Awake()
    {
        currentHealth = enermyData.stats.health;
        maxHealth = enermyData.stats.health;
    }

    public void ChangeHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
