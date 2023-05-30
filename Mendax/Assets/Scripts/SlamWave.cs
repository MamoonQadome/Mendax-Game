using UnityEngine;

public class SlamWave : MonoBehaviour
{
    // Start is called before the first frame update
    public float attackDamage;
    public LayerMask attackables;
    new SpriteRenderer renderer;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(10 * Time.deltaTime, 0, 0));

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, renderer.bounds.size.x, attackables);

        foreach (Collider2D enemy in enemies)
        {
            //call take damage method in enemy script with player attack damage as parameter
            if(enemy.name =="Boss") enemy.GetComponent<BossAI>().TakeDamage(attackDamage); 
            else
                enemy.GetComponent<EnemyInteraction>().TakeDamage(attackDamage);
            
            Debug.Log("Hit");
        }
    }
}
