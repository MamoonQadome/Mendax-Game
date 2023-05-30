using UnityEngine;

public class BossAI : MonoBehaviour
{
    Animator animator;
    Collider2D meleeRange;
    public GameObject projectile;
    public GameObject shot;
    public float knockback = 10f;
    public float meleeDamage = 30f;
    public float health = 100f;
    public float nextAttack;
    public GameObject panel;

    void Start()
    {
        animator = GetComponent<Animator>();
        meleeRange = transform.GetChild(0).GetComponent<Collider2D>();
        nextAttack = Time.time;
    }

    public void MeleeStart()
    {
        meleeRange.enabled = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Shield"))
        {
            return;
        }
        if(collision.CompareTag("Player"))
        {
            var code = collision.GetComponent<KnightInteraction>();
            if (code != null)
            {
                if (!collision.GetComponent<KnightInteraction>().isInvincible)
                {
                    collision.GetComponent<KnightInteraction>().TakeDamage(meleeDamage);
                    collision.GetComponent<PlayerMovement>().knockback = (transform.eulerAngles.y == 0 ? knockback : -knockback);
                }
            }
            else if (!collision.GetComponent<ArcherInteraction>().isInvincible)
            {
                collision.GetComponent<ArcherInteraction>().TakeDamage(meleeDamage);
                collision.GetComponent<ArcherMovement>().knockback = (transform.eulerAngles.y == 0 ? knockback : -knockback);
            } 
        }
        Debug.Log("player hit");
    }
    public void MeleeStop()
    {
        meleeRange.enabled = false;
    }

    public void Shoot()
    {
        Instantiate(shot, transform.position, transform.rotation);
    }

    public void Throw()
    {
        Instantiate(projectile, transform.position, transform.rotation);
    }
    public void UpdateNextAttack()
    {
        nextAttack = Time.time + 4f;
        animator.SetFloat("nextAttack", nextAttack);
    }
    public void TakeDamage(float damage, bool damageOverTime = false)
    {
        
        health -= damage;
        if (health <= 0)
        {
            panel.SetActive(true);
            //play death animation and final cutscene
        }
    }
}
