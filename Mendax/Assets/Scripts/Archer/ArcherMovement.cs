using System.Collections;
using UnityEngine;

public class ArcherMovement : MonoBehaviour
{
    //declaration variables
    Rigidbody2D rb;
    Vector2 input;
    bool isGrounded;
    public float speed = 10f;
    public float jumpForce = 5f;
    public float knockback = 0f;
    Vector2 headPosition;
    Vector2 mousePosition;
    Vector2 aimDirection;
    Vector2 dashDirection;
    TrailRenderer trlRndr; // to give a small animation to a way we are dashing 
    public float dashingVel = 10.0f; // to dash vertical or horizantal
    public float dashingTime = 0.2f; // dashing dur from start to end 
    public float dashCooldownDur = 5f;
    public bool isDashing ; // to know if it's already in the dash or not 
    public bool dashIsRdy = true; // to know if it's ready to use again or not 
    Animator animator;

    void Start()
    {
        //initializing Rigidbody variable with the one attatched to this game object
        rb = GetComponent<Rigidbody2D>();
        trlRndr = GetComponent<TrailRenderer>();
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        //storing the user input in a vector2
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical"));
        headPosition = transform.position;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = mousePosition - headPosition;
        transform.GetChild(0).transform.right = aimDirection.normalized;

        if(aimDirection.x < 0)
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        if(aimDirection.x > 0)
            transform.eulerAngles = Vector3.zero;

        if(Input.GetKeyDown(KeyCode.Space) && dashIsRdy&&GetComponent<ArcherInteraction>().unlockedAbilities[0]==true)
        {
            isDashing = true; 
            dashIsRdy  = false;
            trlRndr.emitting = true ;
            dashDirection = (rb.velocity.x >= 0 ? Vector2.right : Vector2.left);
            StartCoroutine(StopDashing());
        }

        if(isDashing){
            rb.velocity = dashDirection * dashingVel;
            return;
        }
        
    }

    private void FixedUpdate()
    {
        if (isDashing)
            return;
        knockback = Mathf.Lerp(knockback, 0f, 0.2f);
        //Horizontal movement is done with velocity method 
        rb.velocity = new Vector2(input.x * speed + knockback, rb.velocity.y);
        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        //Jumping is done with addforce method after checking the user input and if the object is standing on the ground
        if(isGrounded && input.y==1)
            rb.AddForce(new Vector2(0, input.y * jumpForce), ForceMode2D.Impulse);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //setting the boolean to true if a collision with "ground" was detected 
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //setting the boolean to false if a collision with "ground" was detected
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    private IEnumerator StopDashing(){
        GetComponent<ArcherInteraction>().TakeDamage(0f, dashingTime + 0.1f);
        yield return new WaitForSeconds(dashingTime); 
        trlRndr.emitting = false; 
        isDashing = false;
        Invoke(nameof(DashCoolDown), dashCooldownDur); // calling DashCoolDwon after 7 seconds (wehre cooldown of the dash is 7 seconds )
    }
    private void DashCoolDown() => dashIsRdy = true; // to switch the dashIsRdy to true 
}
