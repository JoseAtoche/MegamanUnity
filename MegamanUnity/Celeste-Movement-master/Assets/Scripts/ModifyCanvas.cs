using UnityEngine;

public class ModifyCanvas : MonoBehaviour
{
    public GameObject barra;

    public AudioClip musicaboss;
    public GameObject sonido;
    public AudioSource sonidoBoss;


    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.GetComponent<BoxCollider2D>().isTrigger == true)
        {
            // GameObject.FindGameObjectWithTag("Gardar").GetComponent<GuardadoAutomatico>().prohibidoguardar = true;
            Vector3 vector = new Vector3(880, 240, 0);
            barra.transform.position = vector;
            sonidoBoss.PlayOneShot(musicaboss);
            sonido.SetActive(false);
            this.GetComponent<BoxCollider2D>().isTrigger = false;

        }




    }
}