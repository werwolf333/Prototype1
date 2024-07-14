using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy : Unit
{
    protected void Run(Vector3 currentPosition, Vector3 nextPoint)
    {
        Vector3 direction = nextPoint - currentPosition;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                orientation = "side_right";
                spriteRenderer.flipX = false;
            }
            else
            {
                orientation = "side_left";
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                orientation = "back";
                spriteRenderer.flipX = false;
            }
            else
            {
                orientation = "front";
                spriteRenderer.flipX = false;
            }
        }
        AnimationRun();
    }

    protected void AnimationRun()
    {
        var startClip = "";
        if (orientation == "front")
        {
            startClip = "run_front";
        }

        if (orientation == "back")
        {
            startClip = "run_back";
        }

        if (orientation == "side_left")
        {
            startClip = "run_side";
            spriteRenderer.flipX = true;
        }
        if (orientation == "side_right")
        {
            startClip = "run_side";
        }  
        animator.Play(startClip);  
    }
}
