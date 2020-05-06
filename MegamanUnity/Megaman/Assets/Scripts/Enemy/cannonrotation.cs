using UnityEngine;

public class cannonrotation : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update

    /// <summary>
    /// Calcula en todo momento la rotacion del cañon
    /// </summary>
    private void Update()
    {
        Vector2 posicionJugador = player.transform.position;
        Vector3 posiconCannon = new Vector3(posicionJugador.x, posicionJugador.y, transform.position.z);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y,
            Mathf.Atan2((posiconCannon.x - transform.position.x),
            -(posiconCannon.y - transform.position.y)) * Mathf.Rad2Deg);
    }
}