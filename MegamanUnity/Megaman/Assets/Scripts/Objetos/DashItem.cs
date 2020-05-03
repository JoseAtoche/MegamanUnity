using UnityEngine;

public class DashItem : MonoBehaviour
{
    /// <summary>
    /// Si el jugador coge el item le permite dar un salto.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)

    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().saltos = 1;

        Destroy(gameObject);
    }
}