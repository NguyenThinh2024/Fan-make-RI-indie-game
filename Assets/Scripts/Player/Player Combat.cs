using UnityEngine;
using Player_Model;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enermyLayer; 
    public Animator anim;
    public float stuntime = .3f;
    public float KnockbackForce = 50;
    public float knockbackTime = .15f;
    private PlayerData playerData;
    private void Start()
    {
        playerData = GetComponent<PlayerData>();
    }

    public void Attack()
    {
        anim.SetBool("isAttack", true);
    }

    public void dealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, 0.5f, enermyLayer);
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Enermy_health>().ChangeHealth(-playerData.strength);
            enemy.GetComponent<Enermy_Knockback>().knockBack(transform, KnockbackForce, knockbackTime ,stuntime);
        }
    }
    public void StopAttack()
    {
        anim.SetBool("isAttack", false);
    }
    public void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, 0.5f);
    }
}
