using UnityEngine;

public class vida : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().vida < 100)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().vida = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().vida + 10;


            }
            Destroy(gameObject);

        }
    }
}
