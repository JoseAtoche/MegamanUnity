using UnityEngine;


/// <summary>
/// Se encarga de hacer respawn a todos los objetos que falten
/// </summary>
public class respawn : MonoBehaviour
{
    public GameObject dash1;
    public GameObject dash2;
    public GameObject dash3;

    public GameObject dashPrefab;
    private Vector3 posicionDash1;
    private Vector3 posicionDash2;
    private Vector3 posicionDash3;

    private float time;

    /// <summary>
    /// Posiciones de los objetos
    /// </summary>
    private void Start()
    {
        posicionDash1 = dash1.transform.position;
        posicionDash2 = dash2.transform.position;
        posicionDash3 = dash3.transform.position;
    }

    /// <summary>
    /// Si detecta que alguna de los tanques ha desaparecido a los 5 segundos pone otra en la misma ubicacion
    /// </summary>
    private void Update()
    {
        if (dash1 == null || dash2 == null || dash3 == null)
        {
            time += Time.deltaTime;
            //Si han pasado más de 5 segundos
            if (time >= 5)
            {
                if (dash1 == null)
                {
                    dash1 = Instantiate(dashPrefab, posicionDash1, new Quaternion(0, 0, 0, 0));
                }

                if (dash2 == null) { dash2 = Instantiate(dashPrefab, posicionDash2, new Quaternion(0, 0, 0, 0)); }

                if (dash3 == null) { dash3 = Instantiate(dashPrefab, posicionDash3, new Quaternion(0, 0, 0, 0)); }

                time -= 5;
            }
        }
    }
}