using UnityEngine;

public class EnemyGroundIzquierda : MonoBehaviour
{
    public EnemyFollow follow;

    private void OnTriggerStay2D(Collider2D collision)
    {
        follow = transform.GetComponentInParent<EnemyFollow>();

        
    }
}