using UnityEngine;

/// <summary>
/// Clase que se encarga de proporcionar ciertos objetos a los Game Object que lo requieran
/// </summary>
public class objetosNecesarios : MonoBehaviour
{
    public FuerzaItem fuerza;
    public GameObject guardado;
    public Collider2D colisionReal;
    public Collider2D colisionEspadaIzquierda;
    public Collider2D colisionEspadaDerecha;
}