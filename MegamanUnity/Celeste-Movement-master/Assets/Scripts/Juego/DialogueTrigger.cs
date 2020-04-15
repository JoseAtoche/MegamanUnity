using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public BossController controlador;

    /// <summary>
    /// Comienza el diálogo
    /// </summary>
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    /// <summary>
    /// Activa el movimiento del jugador y lo pone en la posicion deseada para comenzar la batalla
    /// </summary>
    public void PermitirMovimiento()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(6.18f, -0.49f, 0);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = true;
        controlador.enabled = true;
    }
}