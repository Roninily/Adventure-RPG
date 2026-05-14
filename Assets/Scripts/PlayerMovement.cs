using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("移动设置")]
    public float speed = 5f;
    public Rigidbody2D rb;
    public Animator anim;
    public int facingDirection = 1;

    private Vector2 movement;

    void Update()
    {
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
