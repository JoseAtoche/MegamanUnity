using UnityEngine;

[System.Serializable]
public class DatosCheckPoint
{
    // Start is called before the first frame update
    public int vida;

    public float x;
    public float y;
    public float z;

    public DatosCheckPoint(GameObject p)
    {
        vida = p.GetComponent<Entity_life>().vida;
        x = p.transform.position.x;
        y = p.transform.position.y;
        z = p.transform.position.z;
    }
}