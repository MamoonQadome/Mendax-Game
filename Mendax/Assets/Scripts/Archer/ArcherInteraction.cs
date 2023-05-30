using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcherInteraction : PlayerInteraction
{
    public GameObject[] Arrow;
    public GameObject Bolt;
    public float arrowLaunchForce;
    public float boltLaunchForce;
    private float nextArrowsShoot;
    public float arrowShootRate = 2.0f;
    public float crossbowStaminaCost;
    private bool crossbowCooldown = false;
    private bool quiverChangeCooldown = false;
    public bool isPoisoned = false;
    Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        attackPoint = transform.GetChild(0);
         if(SceneManager.GetActiveScene().buildIndex==4)
        {
            unlockedAbilities = new bool[] { true, false, false };
        }

        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            unlockedAbilities = new bool[] { true, true, false };
        }

        if (SceneManager.GetActiveScene().buildIndex == 8|| SceneManager.GetActiveScene().buildIndex == 10)
        {
            unlockedAbilities = new bool[] { true, true, true };
        }
        
        animator=GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !quiverChangeCooldown&&unlockedAbilities[1]==true)
            QuiverChange();

        if (Time.time >= nextArrowsShoot)
        {
            if(Input.GetMouseButtonDown(0))
            {
                animator.SetBool("normal", true); 
                nextArrowsShoot = Time.time +1f / arrowShootRate;
            }
            if((Input.GetMouseButtonDown(1)) && !crossbowCooldown && unlockedAbilities[2] == true)
            {
                animator.SetBool("crossbow", true);
                nextArrowsShoot = Time.time +1f / arrowShootRate;
            }
        }
        RegainStamina();
    }

    protected override void OnLeftClick()
    {
        if (currentStamina < normalStaminaCost)
            return;
        GameObject ArrowIns = Instantiate(isPoisoned?Arrow[1]:Arrow[0],attackPoint.position,attackPoint.rotation);
        //force the arrow we created 
        ArrowIns.GetComponent<Rigidbody2D>().AddForce(attackPoint.right * arrowLaunchForce );
        if(isPoisoned){
                ArrowIns.GetComponent<ArcherProjectile>().type = 1;
                Debug.Log("Poisoned");
             }   
        //destroy object after 2 second 
        Destroy(ArrowIns,2.0f);

        currentStamina -= normalStaminaCost;
    }

    protected override void OnRightClick()
    {
        if (crossbowCooldown || currentStamina < crossbowStaminaCost) 
            return;
        GameObject crossBow = Instantiate(Bolt,attackPoint.position,attackPoint.rotation);
        //force the crossBow we created 
        crossBow.GetComponent<Rigidbody2D>().AddForce(attackPoint.right * boltLaunchForce);
        crossBow.GetComponent<ArcherProjectile>().type = 2;
        //destroy object after 3 second 
        Destroy(crossBow,3.0f);
        crossbowCooldown = true;
        Invoke(nameof(CrossbowCooldown), 5.0f);

        currentStamina -= crossbowStaminaCost;
    }

    private void CrossbowCooldown() => crossbowCooldown = false;

    private void QuiverChange()
    {
        isPoisoned = !isPoisoned;
        quiverChangeCooldown = true;
        Invoke(nameof(QuiverReady), 5.0f);
    }

    private void QuiverReady() => quiverChangeCooldown = false;

    private void ResetAttacks()
    {
        animator.SetBool("normal", false);
        animator.SetBool("crossbow", false);
    }
}