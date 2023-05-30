using UnityEngine;

public class FlyerDummy : MonoBehaviour
{

    public float aggro_rang;
    Transform Player;
    private Rigidbody2D rb;
    public float speed;
    public float knockback = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void FixedUpdate()
    {
        knockback = Mathf.Lerp(knockback, 0, 0.2f);
        Vector2 distance = Player.position - transform.position;
        Vector2 direction = distance.normalized;
        
        if (distance.SqrMagnitude() <= aggro_rang * aggro_rang)
        {
            rb.velocity = direction * speed;
            rb.velocity += new Vector2(knockback, 0);
        }
        if(distance.x>0)
        {
            transform.eulerAngles = Vector3.zero;
            
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, aggro_rang);
    }
}
