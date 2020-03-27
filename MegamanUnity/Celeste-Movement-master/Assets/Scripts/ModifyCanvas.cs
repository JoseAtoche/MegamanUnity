using UnityEngine;

public class ModifyCanvas : MonoBehaviour
{
    public GameObject barra;

    public AudioClip musicaboss;
    public GameObject sonido;
    public AudioSource sonidoBoss;
    public GameObject muro;
    bool permitirmovimiento = false;


    // Start is called before the first frame update
    private void Start()
    {
    }

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
        if (this.GetComponent<BoxCollider2D>().isTrigger == true)
        {
            //GameObject.FindGameObjectWithTag("Gardar").GetComponent<GuardadoAutomatico>().prohibidoguardar = true;
            barra.transform.position = new Vector3(barra.transform.position.x - 100, barra.transform.position.y, 0);
            sonidoBoss.PlayOneShot(musicaboss);
            sonido.SetActive(false);
            this.GetComponent<BoxCollider2D>().isTrigger = false;
            permitirmovimiento = true;



        }


    }
}