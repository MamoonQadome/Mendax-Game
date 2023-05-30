using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
	{
		collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		_ = collision.GetComponent<PlayerMovement>() != null ? collision.GetComponent<PlayerMovement>().enabled = false : collision.GetComponent<ArcherMovement>().enabled = false;
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

}
