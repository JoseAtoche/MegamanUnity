using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;

    float currentTime = 10;
    float maxTime = 3;
    int veces;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);






    }




    public void permitirMovimiento()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(6.18f, -0.49f, 0);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = true;


    }

}
