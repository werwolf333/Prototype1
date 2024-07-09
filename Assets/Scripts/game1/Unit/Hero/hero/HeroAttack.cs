using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Hero : Unit
{
    protected Coroutine attackSwordCoroutine;
    protected Coroutine attackShieldCoroutine;

    void ToAttack()
    {
        busyAnimator = true;
        if(isArmed)
        {
            var clipLength = AnimationArmedAttack();
            AnimationSwordAttack();
            AnimationShieldAttack();
            Invoke("SetBusyAnimatorFalse", clipLength);
        }
        else
        {
            var clipLength = AnimationUnarmedAttack();
            Invoke("SetBusyAnimatorFalse", clipLength);
        }
        var enemiesInAttack = attackComponent.enemiesInAttack;
        foreach (var enemy in enemiesInAttack)
        {
            var enemyComponent = enemy.GetComponent<Enemy>();
            enemyComponent.TakeDamage(damage);
        }
    }

    float AnimationUnarmedAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        var startClip = "";
        var endClip = "";

        if(orientation == "side_right")
        {
            startClip = "attack_side";
            endClip = "idle_side"; 
        }
        if(orientation == "side_left")
        {
            startClip = "attack_side";
            endClip = "idle_side"; 
        }
        if(orientation == "back")
        {
            startClip = "attack_back";
            endClip = "idle_back"; 
        }
        if(orientation == "front")
        {
            startClip = "attack_front";
            endClip = "idle_front"; 
        }
        attackCoroutine = StartCoroutine(WaitAndPlayIdle(startClip, endClip, 0));
        float clipLength = TimeClip(startClip);
        return clipLength;
    }

    float AnimationArmedAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        var startClip = "";
        var endClip = "";

        if(orientation == "side_right")
        {
            startClip = "attack_armed_side";
            endClip = "idle_armed_side"; 
        }
        if(orientation == "side_left")
        {
            startClip = "attack_armed_side";
            endClip = "idle_armed_side"; 
        }
        if(orientation == "back")
        {
            startClip = "attack_armed_back";
            endClip = "idle_armed_back"; 
        }
        if(orientation == "front")
        {
            startClip = "attack_armed_front";
            endClip = "idle_armed_front"; 
        }
        attackCoroutine = StartCoroutine(WaitAndPlayIdle(startClip, endClip, 0));
        var clipLength = TimeClip(startClip);
        return clipLength;
    }

    void AnimationSwordAttack()
    {
        if (attackSwordCoroutine != null)
        {
            StopCoroutine(attackSwordCoroutine);
        }
        var startClip = "";
        var endClip = "";

        if(orientation == "side_right")
        {
            startClip = "attack_sword_side";
            endClip = "idle_sword_side"; 
        }
        if(orientation == "side_left")
        {
            startClip = "attack_sword_side";
            endClip = "idle_sword_side"; 
        }
        if(orientation == "back")
        {
            startClip = "attack_sword_back";
            endClip = "idle_sword_back"; 
        }
        if(orientation == "front")
        {
            startClip = "attack_sword_front";
            endClip = "idle_sword_front"; 
        }
        attackSwordCoroutine = StartCoroutine(WaitAndPlayIdle(startClip, endClip, 1));
    }

    void AnimationShieldAttack()
    {
        if (attackShieldCoroutine != null)
        {
            StopCoroutine(attackShieldCoroutine);
        }
        var startClip = "";
        var endClip = "";

        if(orientation == "side_right")
        {
            startClip = "attack_shield_side";
            endClip = "idle_shield_side"; 
        }
        if(orientation == "side_left")
        {
            startClip = "attack_shield_side";
            endClip = "idle_shield_side"; 
        }
        if(orientation == "back")
        {
            startClip = "attack_shield_back";
            endClip = "idle_shield_back"; 
        }
        if(orientation == "front")
        {
            startClip = "attack_shield_front";
            endClip = "idle_shield_front"; 
        }
        attackShieldCoroutine = StartCoroutine(WaitAndPlayIdle(startClip, endClip, 2));
    }
}
