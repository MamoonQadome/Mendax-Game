using System.Collections;
using UnityEngine;

public class ShootingEnemyMovement : GroundEnemyMovement
{
    public GameObject projectile;
    Transform player;
    public float aggro;
    bool attackCooldown = true;
    //SpriteRenderer sp;
    Animator controller;
    float attackLenght;
    bool hasRotated = false;

    void Start()
    {
        Initialize();
    }
    protected override void Initialize()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        //sp = GetComponent<SpriteRenderer>();
        controller = GetComponent<Animator>();
        AnimationClip[] clips = controller.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            if (clip.name.EndsWith("attack"))
                attackLenght = clip.length;
        }
        Vector2[] paths = new Vector2[pathHolder.childCount];
        for (int i = 0; i < paths.Length; i++)
        {
            paths[i] = pathHolder.GetChild(i).position;
        }
        StartCoroutine(FollowPath(paths));
    }
    protected override IEnumerator Attack()
    {
        if ((transform.position - player.position).sqrMagnitude <= aggro * aggro && attackCooldown)
        {
            controller.SetBool("onAttack", true);
            yield return new WaitForSeconds(attackLenght);
            controller.SetBool("onAttack", false);
        }
    }

    public void Shoot()
    {
        Instantiate(projectile, rb.position, transform.rotation);
        attackCooldown = false;
        Invoke(nameof(AllowAttack), 2f);
    }
    public void Rotate()
    {
        if (player.position.x > transform.position.x)
        {
            if (Mathf.Abs(transform.eulerAngles.y) == 180)
            {
                transform.eulerAngles = Vector3.zero;
                hasRotated = true;
            }
        }
        else if (player.position.x < transform.position.x)
        {
            if (Mathf.Abs(transform.eulerAngles.y) == 0)
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                hasRotated = true;
            }
        }
    }
    public void RotateBack()
    {
        if (hasRotated == true)
        {
            transform.eulerAngles = new Vector3(0, (transform.eulerAngles.y + 180) % 360, 0);
            hasRotated = false;
        }
    }
    void AllowAttack() => attackCooldown = true;

    private void OnDrawGizmos()
    {
        Vector3 startposition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startposition;
        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, 0.3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawWireSphere(transform.position, aggro);
    }
}
