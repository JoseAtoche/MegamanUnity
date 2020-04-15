using UnityEngine;

public class FuerzaItem : MonoBehaviour
{
    /// <summary>
    /// Si el jugador entra al trigger cogerá el potenciador que le dará más potencia al disparo
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().disparoPotenciado = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().ActivarBiometal();
            Destroy(gameObject);
        }
    }
}