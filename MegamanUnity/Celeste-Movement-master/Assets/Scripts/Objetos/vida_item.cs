using UnityEngine;

public class vida_item : MonoBehaviour
{

    /// <summary>
    /// Si se choca con el jugador y la vida es menor a 100 le suma 10 de vida, SIEMPRE EL OBJETO SE DESTRUYE
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Entity_life>().vida + 10 >= 100)
            {

                GameObject.FindGameObjectWithTag("Player").GetComponent<Entity_life>().vida = 100;

            }

            else
            {

                GameObject.FindGameObjectWithTag("Player").GetComponent<Entity_life>().vida += 10;

            }


            Destroy(gameObject);
        }

    }
}