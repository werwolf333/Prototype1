using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Hero : Unit
{
    void ToAttack()
    {
        busyAnimator = true;
        if(isArmed)
        {
            var clipLength = AnimationArmedAttack();
            AnimationSwordAttack();
            AnimationShieldAttack();
            CancelInvoke("SetBusyAnimatorFalse");
            Invoke("SetBusyAnimatorFalse", clipLength);
        }
        else
        {
            var clipLength = AnimationUnarmedAttack();
            CancelInvoke("SetBusyAnimatorFalse");
            Invoke("SetBusyAnimatorFalse", clipLength);
        }
        var enemiesInAttack = attackComponent.enemiesInAttack;
        foreach (var enemy in enemiesInAttack)
        {
            var enemyComponent = enemy.GetComponent<Enemy>();
            enemyComponent.targetGoal = gameObject;
            enemyComponent.TakeDamage(damage);
        }
    }

    float AnimationUnarmedAttack()
    {
        var startClip = "";
        if(orientation == "side_right")
        {
            startClip = "attack_side";
        }
        if(orientation == "side_left")
        {
            startClip = "attack_side";
        }
        if(orientation == "back")
        {
            startClip = "attack_back";
        }
        if(orientation == "front")
        {
            startClip = "attack_front";
        }
        float clipLength = TimeClip(startClip);
        animator.Play(startClip, 0, 0f); 
        return clipLength;
    }

    float AnimationArmedAttack()
    {
        var startClip = "";
        if(orientation == "side_right")
        {
            startClip = "attack_armed_side";
        }
        if(orientation == "side_left")
        {
            startClip = "attack_armed_side";
        }
        if(orientation == "back")
        {
            startClip = "attack_armed_back";
        }
        if(orientation == "front")
        {
            startClip = "attack_armed_front";
        }
        float clipLength = TimeClip(startClip);
        animator.Play(startClip, 0, 0f); 
        return clipLength;
    }

    float AnimationSwordAttack()
    {
        var startClip = "";
        if(orientation == "side_right")
        {
            startClip = "attack_sword_side"; 
        }
        if(orientation == "side_left")
        {
            startClip = "attack_sword_side";
        }
        if(orientation == "back")
        {
            startClip = "attack_sword_back";
        }
        if(orientation == "front")
        {
            startClip = "attack_sword_front";
        }
        float clipLength = TimeClip(startClip);
        animator.Play(startClip, 1, 0f); 
        return clipLength;
    }

    float AnimationShieldAttack()
    {
        var startClip = "";
        if(orientation == "side_right")
        {
            startClip = "attack_shield_side";
        }
        if(orientation == "side_left")
        {
            startClip = "attack_shield_side";
        }
        if(orientation == "back")
        {
            startClip = "attack_shield_back";
        }
        if(orientation == "front")
        {
            startClip = "attack_shield_front";
        }
        float clipLength = TimeClip(startClip);
        animator.Play(startClip, 2, 0f); 
        return clipLength;
    }
}
