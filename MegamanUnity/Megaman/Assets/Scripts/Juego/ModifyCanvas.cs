using System;
using UnityEngine;

public class ModifyCanvas : MonoBehaviour
{
    public GameObject barra;

    public AudioClip musicaBoss;
    public GameObject sonido;
    public AudioSource sonidoBoss;
    public GameObject muro;
    private bool permitirMovimiento = false;
    public GameObject cutscene;

    public GameObject prometheus;

    /// <summary>
    /// Mueve el muro cuando se le indique
    /// </summary>
    private void Update()
    {
        if (permitirMovimiento)
        {
            Vector3 objetivo = new Vector3(muro.transform.position.x, -2.14f, muro.transform.position.z);
            float fixedSpeed = 15 * Time.deltaTime;
            muro.transform.position = Vector3.MoveTowards(muro.transform.position, objetivo, fixedSpeed);

            if (muro.transform.position.y == -2.14f)
            {
                permitirMovimiento = false;
            }
        }
    }

    /// <summary>
    /// Si he pasado el muro, se activa la cinemática
    /// </summary>
    /// <param name="collision"></param>

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && this.GetComponent<BoxCollider2D>().isTrigger == true)
        {
            prometheus.SetActive(true);
            prometheus.transform.GetChild(0).GetComponent<BossController>().enabled = false;

            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = false;
            cutscene.SetActive(true);
            try
            {
                GameObject.FindGameObjectWithTag("Guardar").GetComponent<GuardadoAutomatico>().prohibidoGuardar = true;
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            finally
            {
                barra.transform.position = new Vector3(barra.transform.position.x - 90, barra.transform.position.y, 0);
                sonidoBoss.PlayOneShot(musicaBoss);
                sonido.SetActive(false);
                this.GetComponent<BoxCollider2D>().isTrigger = false;
                permitirMovimiento = true;
            }
        }
    }
}