using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Hero : Unit
{
    void ToCritAttack()
    {
        busyAnimator = true;
        if(isArmed)
        {
            var clipLength = AnimationCritAttackArmed();
            AnimatioCritAttackSword();
            AnimationCritAttackShield();
            Invoke("SetBusyAnimatorFalse", clipLength);
        }
        else
        {
            var clipLength = AnimationCritAttackUnarmed();
            Invoke("SetBusyAnimatorFalse", clipLength);
        }
    }

    float AnimationCritAttackUnarmed()
    {
        var startClip = "";
        if (orientation == "front")
        {
            startClip = "crit_attack_front";
            spriteRenderer.flipX = false;
        }

        if (orientation == "back")
        {
            startClip = "crit_attack_back";
            spriteRenderer.flipX = false;
        }

        if (orientation == "side_left")
        {
            startClip = "crit_attack_side";
            spriteRenderer.flipX = true;
        }

        if (orientation == "side_right")
        {
            startClip = "crit_attack_side";
            spriteRenderer.flipX = false;
        }
        float clipLength = TimeClip(startClip);
        animator.Play(startClip, 0, 0f); 
        return clipLength;
    } 

    float AnimationCritAttackArmed()
    {
        var startClip = "";
        if(orientation == "side_right")
        {
            startClip = "crit_attack_armed_side";
        }
        if(orientation == "side_left")
        {
            startClip = "crit_attack_armed_side";
        }
        if(orientation == "back")
        {
            startClip = "crit_attack_armed_back";
        }
        if(orientation == "front")
        {
            startClip = "crit_attack_armed_front";
        }
        animator.Play(startClip, 0, 0f); 
        var clipLength = TimeClip(startClip);
        return clipLength;
    }

    float AnimatioCritAttackSword()
    {
        var startClip = "";
        if(orientation == "side_right")
        {
            startClip = "crit_attack_sword_side";
        }
        if(orientation == "side_left")
        {
            startClip = "crit_attack_sword_side";
        }
        if(orientation == "back")
        {
            startClip = "crit_attack_sword_back";
        }
        if(orientation == "front")
        {
            startClip = "crit_attack_sword_front";
        }
        animator.Play(startClip, 1, 0f); 
        var clipLength = TimeClip(startClip);
        return clipLength;
    }

    float AnimationCritAttackShield()
    {
        var startClip = "";
        if(orientation == "side_right")
        {
            startClip = "crit_attack_shield_side";
        }
        if(orientation == "side_left")
        {
            startClip = "crit_attack_shield_side";
        }
        if(orientation == "back")
        {
            startClip = "crit_attack_shield_back";
        }
        if(orientation == "front")
        {
            startClip = "crit_attack_shield_front";
        }
        animator.Play(startClip, 2, 0f); 
        var clipLength = TimeClip(startClip);
        return clipLength;
    }
}
