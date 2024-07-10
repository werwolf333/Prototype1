using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit : MonoBehaviour
{
    protected string pain_back = "pain_back";
    protected string pain_front = "pain_front";
    protected string pain_side = "pain_side";

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
            var timeToDie = AnimationToDie();
            CancelInvoke("AnimationIdle");
            Invoke("ToDie", timeToDie); 
        }
        else
        {
            float clipLength = AnimationTakeDamage();
            CancelInvoke("AnimationIdle");
            Invoke("AnimationIdle", clipLength);
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
