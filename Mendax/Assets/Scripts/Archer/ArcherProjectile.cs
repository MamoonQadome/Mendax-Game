using UnityEngine;

public class ArcherProjectile : MonoBehaviour
{
    float damage = 15.0f;
    Rigidbody2D rb;
    PolygonCollider2D plyCol;
    public int type = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        plyCol = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        Path();
    }

    void Path()
    {
        if (plyCol.enabled)
        {
            Vector2 direction = rb.velocity;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //applying the angle we calculated
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            if (coll.name == "Zombie")
                coll.GetComponent<GroundEnemyMovement>().knockback = transform.eulerAngles.y == 0 ? 50f : -50f;
            else if (coll.name == "Wizard" || coll.name == "Skeleton")
                coll.GetComponent<ShootingEnemyMovement>().knockback = transform.eulerAngles.y == 0 ? 50f : -50f;
            else if (coll.name == "Bat")
                coll.GetComponent<FlyerDummy>().knockback = transform.eulerAngles.y == 0 ? 50f : -50f;
            else if (coll.name == "BloodBat")
                coll.GetComponent<EnemyAI>().knockback = transform.eulerAngles.y == 0 ? 50f : -50f;
            else if (coll.name == "Boss")
            {
                coll.GetComponent<BossAI>().knockback = transform.eulerAngles.y == 0 ? 50f : -50f;
                if (type == 0)
                    coll.gameObject.GetComponent<BossAI>().TakeDamage(damage);
                else if (type == 1)
                    coll.gameObject.GetComponent<BossAI>().TakeDamage(damage / 3, true);
                else
                    coll.gameObject.GetComponent<BossAI>().TakeDamage(damage * 2);
            }
            else if (type == 0)
                coll.gameObject.GetComponent<EnemyInteraction>().TakeDamage(damage);
            else if (type == 1)
                coll.gameObject.GetComponent<EnemyInteraction>().TakeDamage(damage / 2.5f, true);
            else
                coll.gameObject.GetComponent<EnemyInteraction>().TakeDamage(damage * 2);
            Destroy(gameObject);
        }
        if (coll.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            plyCol.enabled = false;
            Destroy(rb);
        }
    }
}
