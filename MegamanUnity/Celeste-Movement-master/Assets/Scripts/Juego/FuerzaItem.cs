using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuerzaItem : MonoBehaviour
{



    /// <summary>
    /// Si cojo el objeto tendré el cañón potenciado 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().canonpotenciado = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().activarBiometal();
            Destroy(gameObject);


        }




    }
}
