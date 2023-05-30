using UnityEngine;

public class Health : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(-2f, 2f);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(x, 2),ForceMode2D.Impulse);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        if (collision.gameObject.CompareTag("Player"))
        {
            var interaction = collision.gameObject.GetComponent<KnightInteraction>();
            if (interaction != null)
            {
                    interaction.GetComponent<KnightInteraction>().RegainHealth(25f);
            }
            else 
                collision.gameObject.GetComponent<ArcherInteraction>().RegainHealth(25f);
            Destroy(gameObject);
        }
    }
}
