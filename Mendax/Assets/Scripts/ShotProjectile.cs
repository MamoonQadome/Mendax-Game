using UnityEngine;

public class ShotProjectile : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public float damage=15f;
    public float knockback = 50f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject,3f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            var code = collision.GetComponent<KnightInteraction>();
            if (code != null)
            {
                if (!code.isInvincible)
                //call take damage method in enemy script with collision attack damage as parameter
                {
                    collision.GetComponent<PlayerMovement>().knockback = transform.right.x * knockback;
                    code.TakeDamage(damage);
                }

            }
            else if (!collision.GetComponent<ArcherInteraction>().isInvincible)
            {
                collision.GetComponent<ArcherMovement>().knockback = transform.right.x * knockback;
                collision.GetComponent<ArcherInteraction>().TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }

}
