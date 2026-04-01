using UnityEngine;
using Enemy_SO_Model;
using Player_Model;
public class Enermy_Combat : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private Enemy_SO enermyData;
    
    [Header("Attack Settings")]
    public Transform attackPoint;
    public LayerMask playerLayer;
    private float enermy_damage;
    private float enermy_attackRange;
    public float knockbackForce;
    public float stunTime;
    private void Awake()
    {
        enermy_damage = enermyData.stats.damage;
        enermy_attackRange = enermyData.attackRange;
    }
    public void attackingPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, enermy_attackRange, playerLayer);
        foreach (Collider2D hit in hits)
        {
            PlayerData player = hit.GetComponent<PlayerData>();
            if (player != null)
            {
                player.ChangeHealth(-enermy_damage);
            }
            PlayerMovement playerMovement = hit.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.knockBack(transform, knockbackForce, stunTime);
            }
        }
    }
}
