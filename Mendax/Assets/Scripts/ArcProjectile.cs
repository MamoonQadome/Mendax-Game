using UnityEngine;

public class ArcProjectile : MonoBehaviour
{
    Transform player;
    
    public float damage;
    public float knockback = 50f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 distance = player.position - transform.position;
        distance.y += 7f;
        distance.x *= 0.7f;
        GetComponent<Rigidbody2D>().AddForce(distance, ForceMode2D.Impulse);
        

    }
    private void Update()
    {
        transform.Rotate(Vector3.forward * -300 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var code = collision.GetComponent<KnightInteraction>();
            if (code != null)
            {
                if (!code.isInvincible)
                //call take damage method in enemy script with collision attack damage as parameter
                {
                    collision.GetComponent<PlayerMovement>().knockback = GetComponent<Rigidbody2D>().velocity.x<0? -knockback: knockback;
                    
                    code.TakeDamage(damage);
                }

            }
            else if (!collision.GetComponent<ArcherInteraction>().isInvincible)
            {
                collision.GetComponent<ArcherMovement>().knockback = GetComponent<Rigidbody2D>().velocity.x < 0 ? -knockback : knockback;
                collision.GetComponent<ArcherInteraction>().TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
