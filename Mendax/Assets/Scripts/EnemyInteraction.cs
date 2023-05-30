using UnityEngine;
using System.Collections;

public class EnemyInteraction : MonoBehaviour
{
    public float maxHealth = 100; //enemy starting health
    float currentHealth; //enemy current health
    public float attackDamage; //enemies damage
    public LayerMask playerLayer;
    public GameObject healthPotion;
    public float knockback = 50f;
    void Start()
    {
        currentHealth = maxHealth; //initializing the health of enemy
    }


    private void Update()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, 0.5f, playerLayer);

        foreach (Collider2D player in players)
        {
            var code = player.GetComponent<KnightInteraction>();
            if (code != null)
            {
                if (!code.isInvincible)
                {
                    player.GetComponent<PlayerMovement>().knockback = transform.right.x * knockback;
                    code.TakeDamage(attackDamage);
                }
            }
            else if (!player.GetComponent<ArcherInteraction>().isInvincible)
            {
                player.GetComponent<ArcherMovement>().knockback = transform.right.x * knockback;
                player.GetComponent<ArcherInteraction>().TakeDamage(attackDamage);
            }
        }//end foreach()
        
    }

    public void TakeDamage(float damage, bool damageOverTime = false) // how the enemy takes damage from external sources
    {
        if (damageOverTime)
        {
            StopAllCoroutines();
            StartCoroutine(PoisonAttack(damage));
        }
        else
        {        //temporary implementation
            currentHealth -= damage;
        }
        if (currentHealth <= 0)
        {
            float chance=Random.Range(0f, 1f);
            if (chance>=0.5f)
            {
                Instantiate(healthPotion, transform.position, transform.rotation);
            }
            if (transform.parent.name.Equals("Enemies"))
                Destroy(gameObject);
            else
                Destroy(transform.parent.gameObject); //built-in method that deletes the referenced gameobject
        }
    }

    IEnumerator PoisonAttack(float damage)
    {
        int frames = 4;
        while (--frames>0)
        {
            currentHealth -= damage;
            yield return new WaitForSeconds(0.5f);
        }

    }





}
