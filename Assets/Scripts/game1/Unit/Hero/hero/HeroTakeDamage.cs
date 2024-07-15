using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Hero : Unit
{
    public void TakeDamageHero()
    {
        currentAnimation = "AnimationTakeDamageUnarmed";
        busyAnimator = true;
        if(isArmed)
        {
            var clipLength = AnimationTakeDamageArmed();
            AnimationTakeDamageSword();
            AnimationTakeDamageShield();
            Invoke("SetBusyAnimatorFalse", clipLength);
        }
        else
        {
            var clipLength = AnimationTakeDamageUnarmed();
            Invoke("SetBusyAnimatorFalse", clipLength);
        }
    }

    float AnimationTakeDamageUnarmed()
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

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if(currentClip == "")
        {
            currentClip = startClip;
            animator.Play(startClip, 0);
        }
        currentAnimationTime = Mathf.Repeat(stateInfo.normalizedTime, 1f);
        if(currentClip != startClip)
        {
           animator.Play(startClip, 0, currentAnimationTime); 
        }
        float clipLength = TimeClip(startClip);
        return clipLength;
    }
    
    float AnimationTakeDamageArmed()
    {
        var startClip = "";
        if(orientation == "side_right")
        {
            startClip = "pain_armed_side";
        }
        if(orientation == "side_left")
        {
            startClip = "pain_armed_side";
        }
        if(orientation == "back")
        {
            startClip = "pain_armed_back";
        }
        if(orientation == "front")
        {
            startClip = "pain_armed_front";
        }
        animator.Play(startClip);
        var clipLength = TimeClip(startClip);
        return clipLength;
    }

    float AnimationTakeDamageSword()
    {
        var startClip = "";
        if(orientation == "side_right")
        {
            startClip = "pain_sword_side";
        }
        if(orientation == "side_left")
        {
            startClip = "pain_sword_side";
        }
        if(orientation == "back")
        {
            startClip = "pain_sword_back";
        }
        if(orientation == "front")
        {
            startClip = "pain_sword_front";
        }
        animator.Play(startClip);
        var clipLength = TimeClip(startClip);
        return clipLength;
    }

    float AnimationTakeDamageShield()
    {
        var startClip = "";
        if(orientation == "side_right")
        {
            startClip = "pain_shield_side";
        }
        if(orientation == "side_left")
        {
            startClip = "pain_shield_side";
        }
        if(orientation == "back")
        {
            startClip = "pain_shield_back";
        }
        if(orientation == "front")
        {
            startClip = "pain_shield_front";
        }
        animator.Play(startClip);
        var clipLength = TimeClip(startClip);
        return clipLength;
    }
}
