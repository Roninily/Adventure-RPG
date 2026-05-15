using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("移动设置")]
    public float speed = 5f;
    public Rigidbody2D rb;
    public Animator anim;
    public int facingDirection = 1;

    private Vector2 movement;
    private bool isAttacking = false;

    void Update()
    {
        if (isAttacking)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                isAttacking = false;
            }
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x > 0 && transform.localScale.x < 0 ||
            movement.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        if (anim != null)
        {
            anim.SetFloat("horizontal", Mathf.Abs(movement.x));
            anim.SetFloat("vertical", Mathf.Abs(movement.y));
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (anim != null)
            {
                anim.SetTrigger("Attack");
                isAttacking = true;
                movement = Vector2.zero;
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement.normalized * speed;
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
