using System.Collections;
using UnityEngine;

public class Enermy_Knockback : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform player;
    private Enermy_movement enermyMovement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enermyMovement = GetComponent<Enermy_movement>();
    }

    public void knockBack(Transform player, float force, float knockbackTime, float stunTime)
    {
        enermyMovement.ChangeState(Enermy_movement.EnemyState.Knockback);
        StartCoroutine(stunTimer(knockbackTime, stunTime));
        Vector2 knockbackDirection = (transform.position - player.position).normalized;
        rb.linearVelocity= knockbackDirection * force;
    }

    IEnumerator stunTimer( float knockbackTime, float stunTime)
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        enermyMovement.ChangeState(Enermy_movement.EnemyState.Idle);
    }
}
