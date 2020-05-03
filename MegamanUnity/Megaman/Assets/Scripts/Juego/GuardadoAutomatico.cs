using System.Collections;
using UnityEngine;

public class GuardadoAutomatico : MonoBehaviour
{
    private float tiempoDeEspera = 0;
    public bool prohibidoGuardar = false;
    public bool nuevaPartida = false;
    public GameObject guardado;

    /// <summary>
    /// Esto permite que el objeto siempre se mantenga en el escenario, sea este cual sea
    /// </summary>
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        //Cada 10 segundos el juego guarda automáticamente a no ser que esté prohibido guardar por ejemplo cuando luchas contra el boss
        if (Time.time > tiempoDeEspera && !prohibidoGuardar)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                GuardarPartida.Save(GameObject.FindGameObjectWithTag("Player"));
                Debug.Log("Guardado correctamente");
                StartCoroutine(Guardado());
            }

            tiempoDeEspera = Time.time + 10;
        }
    }

    //Permite cargar la partida siempre que no se haya pulsado nueva partida
    public void Load()
    {
        if (!nuevaPartida)

        {
            DatosCheckPoint data = GuardarPartida.Load();
            Vector3 vector = new Vector3(data.x, data.y, data.z);
            GameObject.FindGameObjectWithTag("Player").transform.position = vector;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Entity_life>().vida = data.vida;

            Debug.Log("Cargado correctamente");
        }
    }

    //Activa la animacion del circulo de guardado girando
    public IEnumerator Guardado()
    {
        GameObject.Find("ObjetosNecesarios").GetComponent<objetosNecesarios>().guardado.SetActive(true);

        yield return new WaitForSeconds(2f);
        GameObject.Find("ObjetosNecesarios").GetComponent<objetosNecesarios>().guardado.SetActive(false);
    }
}