using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Hero : Unit
{
    void TakeDamageHero()
    {
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
        var clipLength = AnimationTakeDamage();
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
