using UnityEngine;

public class DashItem : MonoBehaviour
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
        if (collision.gameObject.tag == "Player" && GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().saltos == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().saltos = 1;
        }
        Destroy(gameObject);
    }
}