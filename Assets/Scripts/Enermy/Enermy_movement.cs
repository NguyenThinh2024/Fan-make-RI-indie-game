using UnityEngine;
using Enemy_SO_Model;
using UnityEngine.XR;
using Unity.VisualScripting;

public class Enermy_movement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform player;
    public Enemy_SO enemyData;
    private int facingDirection = -1;
    public float attackCooldown;
    private float attackCooldownTimer;
    public Transform detectionPoint;
    public LayerMask playerLayer;
    private Animator anim;
    private EnemyState currentState;
    private float attackRange;
    private float detectRange;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (enemyData != null)
        {
            attackRange = enemyData.attackRange;
            detectRange = enemyData.detectRange;
        }
        ChangeState(EnemyState.Idle);
    }

    void Update()
    {
        checkedForPlayer();
        if (currentState != EnemyState.Knockback)
        {
            if (attackCooldownTimer > 0)
            {
                attackCooldownTimer -= Time.deltaTime;
                if (attackCooldownTimer <= 0)
                {
                    rb.linearVelocity = Vector2.zero;
                }
            }
            if (currentState == EnemyState.Chasing)
            {
                Chase();
            }
            else if (currentState == EnemyState.Attacking)
            {
                rb.linearVelocity = Vector2.zero;

                AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
                if (stateInfo.IsName("Attack 01") && stateInfo.normalizedTime >= 1f)
                {
                    ChangeState(EnemyState.Idle);
                }
            }
        }
    }
    private void Chase()
    {
        if (player.position.x > transform.position.x && facingDirection == -1 ||
            player.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();
        }
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * enemyData.stats.speed;
    }

    private void checkedForPlayer()
    {
        if (attackCooldownTimer > 0) return;
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, detectRange, playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform;
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange && attackCooldownTimer <= 0)
            {
                FacePlayer();
                rb.linearVelocity = Vector2.zero;
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }
            else if (distanceToPlayer > attackRange && currentState != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    private void FacePlayer()
    {
        if (player == null) return;
        bool playerOnRight = player.position.x > transform.position.x;
        bool facingRight = facingDirection == 1;
        if ((playerOnRight && !facingRight) || (!playerOnRight && facingRight))
        {
            Flip();
        }
    }
    private void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    public void ChangeState(EnemyState newState)
    {
        if (currentState == newState) return;
        if (currentState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", false);
        }
        else if (currentState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", false);
        }
        else if (currentState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", false);
        }
        //else if (currentState == EnemyState.Knockback)
        //{
        //    anim.SetBool("isKnockback", false);
        //}
        currentState = newState;
        if (currentState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", true);
            anim.Play("Idle", 0, 0f);
        }
        else if (currentState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", true);
            anim.Play("Walk", 0, 0f);
        }
        else if (currentState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", true);
            anim.Play("Attack 01", 0, 0f);
        }
        //else if (currentState == EnemyState.Knockback)
        //{
        //    anim.SetBool("isKnockback", true);
        //    anim.Play("Knockback", 0, 0f);
        //}

    }

    private void OnDrawGizmosSelected()
    {
        if (detectionPoint != null && enemyData != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(detectionPoint.position, enemyData.detectRange);
        }
    }
    public enum EnemyState
    {
        Idle,
        Chasing,
        Attacking,
        Knockback
    }
}
