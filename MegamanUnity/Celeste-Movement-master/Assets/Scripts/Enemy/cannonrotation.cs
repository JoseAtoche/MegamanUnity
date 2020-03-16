using UnityEngine;

public class cannonrotation : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {



        Vector2 posicionjugador = player.transform.position;
        Vector3 posiconcannon = new Vector3(posicionjugador.x, posicionjugador.y, transform.position.z);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y,
            Mathf.Atan2((posiconcannon.x - transform.position.x),
            -(posiconcannon.y - transform.position.y)) * Mathf.Rad2Deg);


    }
}
