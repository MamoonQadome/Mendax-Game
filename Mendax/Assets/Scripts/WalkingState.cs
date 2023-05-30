using UnityEngine;

public class WalkingState : StateMachineBehaviour
{
    GameObject player;
    Rigidbody2D rb;
    public float meleeRange = 1.5f;
    float nextAttack;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        nextAttack = animator.GetFloat("nextAttack");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (Mathf.Abs(player.transform.position.x - animator.transform.position.x) <= meleeRange)
        {
            nextAttack = Time.time + 5f;
            animator.SetTrigger("meleeRange");
        }
        else if(nextAttack<=Time.time)
        {
            nextAttack = Time.time + 5f;
            if (Random.Range(0f, 1f) >= 0.5f)
                animator.SetTrigger("shoot");
            else
                animator.SetTrigger("throw");

        }

        if (player.transform.position.x > animator.transform.position.x)
        {
            animator.transform.eulerAngles = new Vector3(0, 0, 0);
            rb.velocity = new Vector2(4f, rb.velocity.y);
        }
        else if (animator.transform.position.x > player.transform.position.x)
        {
            animator.transform.eulerAngles = new Vector3(0, 180, 0);
            rb.velocity = new Vector2(-4f, rb.velocity.y);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        rb.velocity = Vector2.zero;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
