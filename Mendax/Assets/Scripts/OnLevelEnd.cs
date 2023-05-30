using UnityEngine;
public class OnLevelEnd : MonoBehaviour
{
    public GameObject panel;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        panel.SetActive(true);

    }

}
