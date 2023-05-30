using UnityEngine;
using UnityEngine.SceneManagement;
public class KnightInteraction : PlayerInteraction
{
    public GameObject slam;
    BoxCollider2D col;
    public GameObject shield;
    public LayerMask ground;
    public float slamStaminaCost;
    public float heavyStaminaCost;
    public float normalAttackRate = 2f;
    public float heavyAttackRate = 1f;
    public float slamAttackRate = 0.5f;
    public float leftHoldMin = 1f;
    float nextAttackTime = 0f;
    float finLeftHeldDur = -1f;
    float curLeftHeldDur;
    bool shieldCooldown = true;
    public float attackRange; // weapon attack range
    public float attackDamage; // player attack damage
    Animator controller;

    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        col = GetComponents<BoxCollider2D>()[0];
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
        controller = GetComponent<Animator>();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            curLeftHeldDur = Time.time;
        if (Input.GetMouseButtonUp(0))
            finLeftHeldDur = Time.time - curLeftHeldDur; 
        
        if (Time.time >= nextAttackTime)
        {
            
            if (finLeftHeldDur != -1f)
            {
                if (finLeftHeldDur < leftHoldMin )
                {
                    controller.SetBool("normal", true);

                    nextAttackTime = Time.time + 1f / normalAttackRate;
                    finLeftHeldDur = -1f;
                }
                else if(unlockedAbilities[2] == true)
                {
                    controller.SetBool("slam", true);

                    nextAttackTime = Time.time + 1f / slamAttackRate;
                    finLeftHeldDur = -1f;
                }
            }
            else if (Input.GetMouseButtonDown(1)&& unlockedAbilities[1] == true)
            {
                controller.SetBool("heavy", true);

                nextAttackTime = Time.time + 1f / heavyAttackRate;
            }
        }
        else
            finLeftHeldDur = -1f;
        if (Input.GetKeyDown(KeyCode.E) && shieldCooldown&& unlockedAbilities[0] == true)
            OnShieldRaise();
        RegainStamina();
    }
   
   private void OnDrawGizmos()
    {
        
        Gizmos.DrawSphere(attackPoint.position, attackRange); 
        
    }

    protected override void OnLeftClick()
    {
        if (currentStamina < normalStaminaCost)
            return;
         /*creating imaginary circle that returns a reference to every collider 
         that was inside its range and was part of the "attackable" layers*/
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackables);
         
        foreach(Collider2D enemy in enemies)
        {
            if (enemy.name == "Boss")
            {
                enemy.GetComponent<BossAI>().knockback = transform.eulerAngles.y == 0 ? 50f : -50f;
                enemy.GetComponent<BossAI>().TakeDamage(attackDamage);
            }
            else
            {
                enemy.GetComponent<EnemyInteraction>().TakeDamage(attackDamage);
                if (enemy.name == "Zombie")
                    enemy.GetComponent<GroundEnemyMovement>().knockback = transform.eulerAngles.y == 0 ? 50f : -50f;
                if (enemy.name == "Wizard" || enemy.name == "Skeleton")
                    enemy.GetComponent<ShootingEnemyMovement>().knockback = transform.eulerAngles.y == 0 ? 50f : -50f;
                if (enemy.name == "Bat")
                    enemy.GetComponent<FlyerDummy>().knockback = transform.eulerAngles.y == 0 ? 50f : -50f;
                if (enemy.name == "BloodBat")
                    enemy.GetComponent<EnemyAI>().knockback = transform.eulerAngles.y == 0 ? 50f : -50f;
            }
        }
        currentStamina -= normalStaminaCost;
    }

    protected override void OnRightClick()
    {
        if (currentStamina < heavyStaminaCost)
            return;

        /*creating imaginary circle that returns a reference to every collider 
        that was inside its range and was part of the "attackable" layers*/
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange *1.2f, attackables);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.name == "Boss")
            {
                enemy.GetComponent<BossAI>().knockback = transform.eulerAngles.y == 0 ? 50f : -50f;
                enemy.GetComponent<BossAI>().TakeDamage(attackDamage);
            }
            else
            {//call take damage method in enemy script with player attack damage as parameter
                enemy.GetComponent<EnemyInteraction>().TakeDamage(attackDamage);
                if (enemy.name == "Zombie")
                    enemy.GetComponent<GroundEnemyMovement>().knockback = transform.eulerAngles.y == 0 ? 50f : -50f;
                if (enemy.name == "Wizard" || enemy.name == "Skeleton")
                    enemy.GetComponent<ShootingEnemyMovement>().knockback = transform.eulerAngles.y == 0 ? 50f : -50f;
                if (enemy.name == "Bat")
                    enemy.GetComponent<FlyerDummy>().knockback = transform.eulerAngles.y == 0 ? 50f : -50f;
                if (enemy.name == "BloodBat")
                    enemy.GetComponent<EnemyAI>().knockback = transform.eulerAngles.y == 0 ? 50f : -50f;
            }
        }
        currentStamina -= heavyStaminaCost;
    }

    protected override void OnLeftHold()
    {
        if (currentStamina < slamStaminaCost || !col.IsTouchingLayers(ground))
            return;
        Instantiate(slam, transform.position - new Vector3(0, 1.2f, 0), new Quaternion(0, 0, 0, 0));
        Instantiate(slam, transform.position - new Vector3(0, 1.2f, 0), new Quaternion(0, 180, 0, 0));
        currentStamina -= slamStaminaCost;
    }

    void OnShieldRaise()
    {
        TakeDamage(0, 1f);
        shield.SetActive(true);
        Invoke(nameof(OnShieldLower), 1f);
    }
    void OnShieldLower()
    {
        shieldCooldown = false;
        shield.SetActive(false);
        Invoke(nameof(ShieldAllow), 0.5f);
    }
    void ShieldAllow() => shieldCooldown = true;

    private void ResetAttacks()
    {
        controller.SetBool("normal", false);
        controller.SetBool("heavy", false);
        controller.SetBool("slam", false);
    }
}
