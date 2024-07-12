using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy : Unit
{
    public void TakeDamage(float attack)
    {
        var pureAttack = attack - protection;
        if(pureAttack<0)
        {
            pureAttack = 0;
        }
        health = health - pureAttack;
        if(health <= 0)
        {
            busyAnimator = true;
            var timeToDie = AnimationToDie();
            CancelInvoke("AnimationIdle");
            Invoke("ToDie", timeToDie); 
        }
        else
        {
            busyAnimator = true;
            float clipLength = AnimationTakeDamage();
            CancelInvoke("SetBusyAnimatorFalse");
            Invoke("SetBusyAnimatorFalse", clipLength);
            UpdateOptionTactics(Options.attack);
            Attack();
        }
    }

    protected float AnimationTakeDamage()
    {
        var startClip = "";
        if (orientation == "front")
        {
            startClip = "pain_front";
            spriteRenderer.flipX = false;
        }

        if (orientation == "back")
        {
            startClip = "pain_back";
            spriteRenderer.flipX = false;
        }

        if (orientation == "side_left")
        {
            startClip = "pain_side";
            spriteRenderer.flipX = true;
        }

        if (orientation == "side_right")
        {
            startClip = "pain_side";
            spriteRenderer.flipX = false;
        }
        float clipLength = TimeClip(startClip);
        animator.Play(startClip, 0, 0f); 
        return clipLength;
    }
}
