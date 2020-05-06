using UnityEngine;

public class EnemyGroundIzquierda : MonoBehaviour
{
    public EnemyFollow follow;

    private void OnTriggerStay2D(Collider2D collision)
    {
        follow = transform.GetComponentInParent<EnemyFollow>();

        //    if (follow.rotacion == new Quaternion(0, 180, 0, 0))
        //    {
        //        if (collision.tag != "Ground")
        //        {
        //            Debug.Log("NO TOCA GROUNDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD");
        //            follow.moverderecha = false;

        //        }
        //        else
        //        {
        //            Debug.Log("suelo");
        //            follow.moverderecha = true;

        //        }
        //    }

        //    else
        //    {
        //        if (collision.tag != "Ground")
        //        {
        //            Debug.Log("NO TOCA GROUNDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD");

        //            follow.moverizquierda = false;

        //        }
        //        else
        //        {
        //            Debug.Log("suelo");

        //            follow.moverizquierda = true;

        //        }
        //    }

        //}
    }
}