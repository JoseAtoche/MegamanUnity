using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public BossController controlador;

    /// <summary>
    /// Comienza el di�logo
    /// </summary>
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        controlador.enabled = false;
    }

    /// <summary>
    /// Activa el movimiento del jugador y lo pone en la posicion deseada para comenzar la batalla
    /// </summary>
    public void PermitirMovimiento()
    {
        controlador.enabled = true;
    }

    public void PermitirMovimientoJugador()
    {

        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(136.28f, -15.42f, 0);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = true;


    }
}