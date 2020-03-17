using UnityEngine;

public class ModifyCanvas : MonoBehaviour
{
    public GameObject barra;

    public AudioClip musicaboss;
    public AudioSource sonido;

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
        GameObject.FindGameObjectWithTag("Gardar").GetComponent<GuardadoAutomatico>().prohibidoguardar = true;
        Vector3 vector = new Vector3(880, 240, 0);
        barra.transform.position = vector;
        sonido.PlayOneShot(musicaboss);
    }
}