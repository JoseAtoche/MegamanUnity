using UnityEngine;

public class vida_item : MonoBehaviour
{
    public AudioClip sonido;


    /// <summary>
    /// Si se choca con el jugador y la vida es menor a 100 le suma 10 de vida, SIEMPRE EL OBJETO SE DESTRUYE
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Si la vida del jugador al sumar 10 es mayor a 100 pongo la vida a 100
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Entity_life>().vida + 10 >= 100)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Entity_life>().vida = 100;
            }
            //Si no pues simplemente le sumo 10
            else
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Entity_life>().vida += 10;
            }

            //Destuyo este objeto
            Destroy(gameObject);
            GetComponent<AudioSource>().PlayOneShot(sonido);

        }
    }
}