using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if (GameObject.FindObjectOfType<Movement>().ataque > 0)
        {


            BossController enemy = hitInfo.GetComponent<BossController>();
            if (enemy != null)
            {
                enemy.heart = enemy.heart - 1;
            }
        }

        //  Instantiate(impactEffect, transform.position, transform.rotation);

    }
}
