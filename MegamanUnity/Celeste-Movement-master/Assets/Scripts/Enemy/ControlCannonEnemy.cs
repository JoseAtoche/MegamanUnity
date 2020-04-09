using UnityEngine;

public class ControlCannonEnemy : MonoBehaviour
{
    public float radiodevision;
    public GameObject bala;
    public GameObject cannon;
    public float tiempoinicia = 0;
    public float tiempoespera = 2;
    public GameObject jugador;


    private void Update()
    {
        float dist = Vector3.Distance(jugador.transform.position, transform.position);
        if (cannon == null)
        {

            if (dist < radiodevision)
            {
                tiempoinicia += Time.deltaTime;
                if (tiempoinicia >= tiempoespera)
                {

                    GameObject balacreada = Instantiate(bala, this.transform.position, this.transform.rotation);
                    balacreada.GetComponent<Rigidbody2D>().AddForce(this.transform.position);
                    balacreada.transform.SetParent(transform);
                    tiempoinicia = 0;
                }
            }
        }
        else if (dist < radiodevision)
        {
            tiempoinicia += Time.deltaTime;
            if (tiempoinicia >= tiempoespera)
            {
                GameObject balacreada = Instantiate(bala, cannon.transform.position, cannon.transform.rotation);
                balacreada.GetComponent<Rigidbody2D>().AddForce(cannon.transform.position);
                balacreada.transform.SetParent(transform);
                tiempoinicia = 0;
            }
        }
    }
}