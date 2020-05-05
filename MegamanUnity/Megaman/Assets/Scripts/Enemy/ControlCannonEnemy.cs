using UnityEngine;

public class ControlCannonEnemy : MonoBehaviour
{
    public float radioDeVision;
    public GameObject bala;
    public GameObject cannon;
    public float tiempoInicia = 0;
    public float tiempoEspera = 2;
    public GameObject jugador;
    public AudioClip disparo;

    /// <summary>
    /// Dispara al enemigo cada cierto tiempo si está en su campo de visión
    /// </summary>
    private void Update()
    {
        float dist = Vector3.Distance(jugador.transform.position, transform.position);
        if (cannon == null)
        {
            if (dist < radioDeVision)
            {
                tiempoInicia += Time.deltaTime;
                if (tiempoInicia >= tiempoEspera)
                {
                    GameObject balacreada = Instantiate(bala, this.transform.position, this.transform.rotation);
                    balacreada.GetComponent<Rigidbody2D>().AddForce(new Vector2(10, 10));
                    balacreada.transform.SetParent(transform);
                    tiempoInicia = 0;
                }
            }
        }
        else if (dist < radioDeVision)
        {
            tiempoInicia += Time.deltaTime;
            if (tiempoInicia >= tiempoEspera)
            {
                GameObject balacreada = Instantiate(bala, cannon.transform.position, cannon.transform.rotation);
                balacreada.GetComponent<Rigidbody2D>().AddForce(cannon.transform.position);
                GetComponent<AudioSource>().PlayOneShot(disparo);
                balacreada.transform.SetParent(transform);
                tiempoInicia = 0;
            }
        }
    }
}