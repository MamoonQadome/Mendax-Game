using UnityEngine;

public class BonFire : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger("lightFire");
            transform.GetChild(0).gameObject.SetActive(true);
            var code = collider.GetComponent<KnightInteraction>();
            if (code != null)
            {
                collider.GetComponent<KnightInteraction>().checkpoint = transform;
            }
            else
                collider.GetComponent<ArcherInteraction>().checkpoint = transform;

        }

    }
}
