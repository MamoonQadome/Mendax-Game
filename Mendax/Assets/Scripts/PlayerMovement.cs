using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 input;
    bool isGrounded;
    public float speed = 10f;
    public float jumpForce = 5f;
    Animator controller;
    public float knockback = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<Animator>();
    }

    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(input.x < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if (input.x > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        controller.SetFloat("speed", Mathf.Abs(input.x));
    }

    private void FixedUpdate()
    {
        knockback = Mathf.Lerp(knockback, 0f, 0.2f);
        rb.velocity = new Vector2(input.x * speed + knockback, rb.velocity.y);
        
        if(isGrounded && input.y==1)
            rb.AddForce(new Vector2(0, input.y * jumpForce), ForceMode2D.Impulse);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}
