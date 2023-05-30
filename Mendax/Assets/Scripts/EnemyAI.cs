using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    Transform target;
    Rigidbody2D rb;
    Seeker seeker;
    Path path;
    public float speed = 400f;
    public float nextWaypointDistance = 2f;
    int currentWaypoint = 0;
    bool reachedEndOfPath;
    public float agrro = 15f;
    public float knockback = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        seeker = GetComponent<Seeker>();
        InvokeRepeating(nameof(GenPath), 0.5f, 0.5f);
    }

    void GenPath()
    {
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
            return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
            reachedEndOfPath = false;

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = speed * Time.deltaTime * direction;
        float distance = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).SqrMagnitude();
        if ((rb.position - (Vector2)target.position).SqrMagnitude()>agrro*agrro)
        {
            return;
        }
        rb.AddForce(force);
        knockback = Mathf.Lerp(knockback, 0, 0.2f);
        rb.velocity += new Vector2(knockback, 0);
        if (distance < nextWaypointDistance)
            currentWaypoint++;
        if(force.x>=0.01f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if(force.x<=-0.01f)
            transform.eulerAngles = new Vector3(0f, 180f, 0f);



    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, agrro);

    }
}
