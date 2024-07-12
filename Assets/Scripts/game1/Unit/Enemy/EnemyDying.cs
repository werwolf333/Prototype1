using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy : Unit
{
    protected float AnimationToDie()
    {
        var startClip = "";
        if (orientation == "front")
        {
            startClip = "dying_front";
        }

        if (orientation == "back")
        {
            startClip = "dying_back";
        }

        if (orientation == "side_left")
        {
            startClip = "dying_side";
            spriteRenderer.flipX = true;
        }

        if (orientation == "side_right")
        {
            startClip = "dying_side";
        }
        animator.Play(startClip);
        return TimeClip(startClip);
    }

    protected void ToDie()
    {
        Destroy(gameObject);
    }
}
