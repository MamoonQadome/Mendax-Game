using System.Collections;
using UnityEngine;

public class GroundEnemyMovement : MonoBehaviour
{
    protected Rigidbody2D rb;
    public Transform pathHolder;
    public float speed;
    public float knockback = 0f;

    void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2[] paths = new Vector2[pathHolder.childCount];
        for (int i = 0; i < paths.Length; i++)
        {
            paths[i] = pathHolder.GetChild(i).position;
        }
        StartCoroutine(FollowPath(paths));
    }

    protected IEnumerator FollowPath(Vector2[] paths)
    {
        int nextPosIndex = 1;
        rb.position = paths[0];
        Vector2 nextPosition = paths[nextPosIndex];
        //SpriteRenderer sp = GetComponent<SpriteRenderer>();
        while (true)
        {
            yield return StartCoroutine(Attack());
            knockback = Mathf.Lerp(knockback, 0, 0.2f);
            rb.velocity = new Vector2(knockback, rb.velocity.y);
            rb.position = Vector2.MoveTowards(rb.position, new Vector2(nextPosition.x, rb.position.y), speed * Time.fixedDeltaTime);
            if (rb.position.x == nextPosition.x)
            {
                nextPosIndex = (nextPosIndex + 1) % paths.Length;
                nextPosition = paths[nextPosIndex];
                transform.right *= -1;
               // sp.flipY = !sp.flipY;
            }
            yield return null;
        }

    }
    protected virtual IEnumerator Attack()
    {
        yield return null;
    }
    void OnDrawGizmos()
    {
        Vector3 startposition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startposition;
        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, 0.3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        
    }
}
