using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyCanvas : MonoBehaviour
{
    public GameObject barra;

    public AudioClip musicaboss;
    public AudioSource sonido;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        Vector3 vector = new Vector3(880, 240, 0);
        barra.transform.position = vector;
        sonido.PlayOneShot(musicaboss);



    }
}
