using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit : MonoBehaviour
{
    protected string pain_back = "pain_back";
    protected string pain_front = "pain_front";
    protected string pain_side = "pain_side";

    protected Coroutine takeDamageCoroutine;

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
            Invoke("ToDie", timeToDie); 
        }
        else
        {
            AnimationTakeDamage();
        }
    }

    protected void AnimationTakeDamage()
    {
        var startClip = "";
        var endClip = "";
        if (orientation == "front")
        {
            startClip = "pain_front";
            endClip = "idle_front";
            spriteRenderer.flipX = false;
        }

        if (orientation == "back")
        {
            startClip = "pain_back";
            endClip = "idle_back";
            spriteRenderer.flipX = false;
        }

        if (orientation == "side_left")
        {
            startClip = "pain_side";
            endClip = "idle_side";
            spriteRenderer.flipX = true;
        }

        if (orientation == "side_right")
        {
            startClip = "pain_side";
            endClip = "idle_side";
            spriteRenderer.flipX = false;
        }
        if (takeDamageCoroutine != null)
        {
            StopCoroutine(takeDamageCoroutine);
        }
        takeDamageCoroutine = StartCoroutine(WaitAndPlayIdle(startClip, endClip));
    }
}
