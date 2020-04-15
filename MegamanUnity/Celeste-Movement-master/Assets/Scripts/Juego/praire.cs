using UnityEngine;

public class praire : MonoBehaviour
{
    public GameObject bocadillo;
    public Animator nube;
    private bool activado = false;
    public GameObject texto;
    public GameObject tutorial;

    /// <summary>
    /// Al comenzar hace que la animacion de la nube se agrande
    /// </summary>
    private void Start()
    {
        nube.SetTrigger("grande");
    }

    /// <summary>
    /// Cuando el jugador sale de la zona donde el personaje es interactuable se activa el tutorial y el vuelve a aparecer la nube
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        nube.ResetTrigger("grande");
        nube.ResetTrigger("pequeño");

        nube.SetTrigger("grande");

        if (activado)
        {
            texto.transform.position = new Vector3(texto.transform.position.x, texto.transform.position.y + 400 + 108, 0);
            activado = false;
            tutorial.SetActive(true);
        }
    }

    /// <summary>
    /// Mientras estés en la zona del personaje, si pulsas el botón correcto y siempre que posteriormente no le hayas dado anteriormente aparecerá el texto de personaje por pantalla
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetButtonDown("Fire1") && !activado)
        {
            nube.ResetTrigger("grande");

            nube.SetTrigger("pequeño");
            nube.SetTrigger("pequeño");

            texto.transform.position = new Vector3(texto.transform.position.x, texto.transform.position.y - 400 - 108, 0);
            nube.GetComponent<DialogueTrigger>().TriggerDialogue();
            activado = true;
        }
    }
}