using UnityEngine;

public class EnemyGroundDerecha : MonoBehaviour
{
    public EnemyFollow follow;

    private void OnTriggerStay2D(Collider2D collision)
    {
        follow = transform.GetComponentInParent<EnemyFollow>();
    }
}