using UnityEngine;

public class FadeTrigger : MonoBehaviour
{
    
    private void StartFade()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        var manager = FindObjectOfType<DialogueManager>();
        if (manager !=null)
        manager.GetComponent<DialogueManager>().OnDialogueEnd += StartFade;

    }

    
}
