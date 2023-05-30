using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    GameObject player;
    public Slider heathSlider;
    public Slider staminaSlider;

    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
            

    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<KnightInteraction>() != null)
        {
            heathSlider.value = player.GetComponent<KnightInteraction>().currentHealth;
            staminaSlider.value = player.GetComponent<KnightInteraction>().currentStamina;
        }
        else
        {
            heathSlider.value = player.GetComponent<ArcherInteraction>().currentHealth;
            staminaSlider.value = player.GetComponent<ArcherInteraction>().currentStamina;
        }
    }
}
