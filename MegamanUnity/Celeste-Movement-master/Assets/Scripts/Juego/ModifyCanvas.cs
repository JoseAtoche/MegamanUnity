using UnityEngine;

public class ModifyCanvas : MonoBehaviour
{
    public GameObject barra;

    public AudioClip musicaboss;
    public GameObject sonido;
    public AudioSource sonidoBoss;
    public GameObject muro;
    bool permitirmovimiento = false;
    public GameObject cutscene;

    public GameObject prometheus;

    // Update is called once per frame
    private void Update()
    {

        if (permitirmovimiento)
        {

            Vector3 objetivo = new Vector3(muro.transform.position.x, -2.14f, muro.transform.position.z);
            float fixedSpeed = 15 * Time.deltaTime;
            muro.transform.position = Vector3.MoveTowards(muro.transform.position, objetivo, fixedSpeed);

            if (muro.transform.position.y == -2.14f)
            {
                permitirmovimiento = false;
            }


        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && this.GetComponent<BoxCollider2D>().isTrigger == true)
        {
            prometheus.SetActive(true);

            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = false;
            cutscene.SetActive(true);
            GameObject.FindGameObjectWithTag("Guardar").GetComponent<GuardadoAutomatico>().prohibidoguardar = true;
            barra.transform.position = new Vector3(barra.transform.position.x - 90, barra.transform.position.y, 0);
            sonidoBoss.PlayOneShot(musicaboss);
            sonido.SetActive(false);
            this.GetComponent<BoxCollider2D>().isTrigger = false;
            permitirmovimiento = true;


        }

    }
}