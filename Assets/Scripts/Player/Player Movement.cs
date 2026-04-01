using UnityEngine;
using Player_Model;
using UnityEditor.Experimental.GraphView;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private PlayerData playerData;
    public Rigidbody2D rb;
    public Animator anim;
    public int facingDirection = 1;
    private bool isKnockback;

    public PlayerCombat playerCombat;

    private void Update()
    {
        if(Input.GetButtonDown("Punch"))
        {
            playerCombat.Attack();
            rb.linearVelocity = Vector2.zero;

        }
    }
    void Start()
    {
        playerData = GetComponent<PlayerData>();
        if (playerData == null)
        {
            Debug.LogError("PlayerData component not found on this GameObject!");
        }
    }
    void FixedUpdate()
    {
        if (playerData == null) return;
        if (isKnockback == false)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            if (horizontal > 0 && transform.localScale.x > 0 || horizontal < 0 && transform.localScale.x < 0)
            {
                Flip();
            }
            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));
            rb.linearVelocity = new Vector2(horizontal, vertical) * playerData.speed;
        }
    }
    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    public void knockBack(Transform enemy, float force, float stunTime)
    {
        isKnockback = true;
        Vector2 knockbackDirection = (transform.position - enemy.position).normalized;
        rb.linearVelocity= knockbackDirection * force;
        StartCoroutine(KnockbackCounter(stunTime));
    }
    IEnumerator KnockbackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.linearVelocity = Vector2.zero;
        isKnockback = false;
    }
}
