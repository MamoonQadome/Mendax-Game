using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        var interaction = collision.gameObject.GetComponent<KnightInteraction>();
        if (interaction != null)
        {
            if (!interaction.GetComponent<KnightInteraction>().isInvincible)
                //call take damage method in enemy script with player attack damage as parameter
                interaction.GetComponent<KnightInteraction>().TakeDamage(35);
        }
        else if (!collision.gameObject.GetComponent<ArcherInteraction>().isInvincible)
            collision.gameObject.GetComponent<ArcherInteraction>().TakeDamage(35);


    }
}
