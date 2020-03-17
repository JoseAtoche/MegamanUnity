using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private Animator anim;
    private Movement move;
    private Collision coll;

    [HideInInspector]
    public SpriteRenderer sr;

    private float tiempodeespera;

    private void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponentInParent<Collision>();
        move = GetComponentInParent<Movement>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        anim.SetBool("onGround", coll.onGround);
        anim.SetBool("onWall", coll.onWall);
        // anim.SetBool("onRightWall", coll.onRightWall);
        //anim.SetBool("wallGrab", move.wallGrab);
        anim.SetBool("wallSlide", move.wallSlide);
        //anim.SetBool("canMove", move.canMove);
        anim.SetBool("isDashing", move.isDashing);

        anim.SetBool("Shoot", move.shoot);
        move.shoot = false;

        anim.SetBool("BigAttack", move.bigAtack);
        /// anim.SetBool("OnLeader", move.onleader);
        anim.SetBool("upattack", move.upattack);
        anim.SetInteger("ataque", move.ataque);
    }

    public void SetHorizontalMovement(float x, float y, float yVel)
    {
        anim.SetFloat("HorizontalAxis", x);
        anim.SetFloat("VerticalAxis", y);
        anim.SetFloat("VerticalVelocity", yVel);

        if (x != 0)
        {
            anim.SetBool("Moviendose", true);
            tiempodeespera = Time.time;
        }

        if (Time.time > tiempodeespera + 0.03)
        {
            anim.SetBool("Moviendose", false);
        }
    }

    public void SetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    public void Flip(int side)
    {
        if (move.wallGrab || move.wallSlide)
        {
            if (side == -1 && sr.flipX)
            {
                return;
            }

            if (side == 1 && !sr.flipX)
            {
                return;
            }
        }

        bool state = (side == 1) ? false : true;
        sr.flipX = state;
    }
}