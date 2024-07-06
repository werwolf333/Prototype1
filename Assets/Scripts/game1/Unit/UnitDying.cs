using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit : MonoBehaviour
{
    protected string dying_back = "dying_back";
    protected string dying_front = "dying_front";
    protected string dying_side = "dying_side";

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
