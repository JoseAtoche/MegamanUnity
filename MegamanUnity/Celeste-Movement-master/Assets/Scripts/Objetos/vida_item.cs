﻿using UnityEngine;

public class vida_item : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Entity_life>().vida < 100)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Entity_life>().vida = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity_life>().vida + 10;


            }
            Destroy(gameObject);

        }
    }
}
