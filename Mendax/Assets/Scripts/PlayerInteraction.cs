using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerInteraction : MonoBehaviour
{
    protected Transform attackPoint; //transform component of the weapon Center
    public LayerMask attackables; //groups which the player can apply damage to
    public const float maxHealth = 100f;// player max health
    public const float maxStamina = 100f;
    public float staminaRegainRate;
    public float normalStaminaCost;
    public float currentStamina;
    public float currentHealth;// player current health
    public  bool isInvincible;
    public bool[] unlockedAbilities;
    public Transform checkpoint;
    
    protected virtual void OnLeftClick()
    {    
        
    }//end OnLeftClick()

    protected virtual void OnRightClick()
    {


    }//end OnRightClick()

    protected virtual void OnLeftHold()
    {

    }//end OnLeftHold

    public void TakeDamage(float dmg, float invincibilityDur = 2f){
        currentHealth -= dmg;
        if(currentHealth<=0)
        {
            OnDeath();
            return;
        }
        isInvincible = true;
        Invoke(nameof(RemoveInvincibility), invincibilityDur);

    }//end TakeDamage()

    void RemoveInvincibility() => isInvincible = false;

    public void RegainHealth(float amount){
        currentHealth += amount;
        //to the keep current health less than or equal the max health
        currentHealth = Mathf.Clamp(currentHealth,1,maxHealth); 
    }//end RegainHealth()

    protected void RegainStamina()
    {
        currentStamina += staminaRegainRate;
        //to keep the current stamina less than or equal the max stamina
        currentStamina = Mathf.Clamp(currentStamina,0,maxStamina); 
    }
    public virtual void OnDeath() 
    {
        
        if(checkpoint != null)
            CheckpointManager.checkpoint =new float[]{ checkpoint.position.x , checkpoint.position.y};
        SceneManager.LoadScene(CheckpointManager.level);
    }//end OnDeath()

}//end class
