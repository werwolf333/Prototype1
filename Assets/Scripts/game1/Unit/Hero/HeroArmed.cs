using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Hero : Unit
{
    protected string idle_armed_back = "idle_armed_back";
    protected string idle_armed_front = "idle_armed_front";
    protected string idle_armed_side = "idle_armed_side";
    private GameObject sword;
    private GameObject shield;
    private SpriteRenderer spriteRendererSword;
    private SpriteRenderer spriteRendererShield;
    protected Coroutine attackSwordCoroutine;
    protected Coroutine attackShieldCoroutine;

    void PreStart()
    {
        sword = transform.Find("sword").gameObject;
        shield = transform.Find("shield").gameObject;
        spriteRendererSword = sword.GetComponent<SpriteRenderer>();
        spriteRendererShield = shield.GetComponent<SpriteRenderer>();
        if(!isArmed)
        {
            sword.SetActive(false);
            shield.SetActive(false);
        }
    }

    void ToStayArmed()
    {
        AnimationHero("idle_armed");
        AnimationSword("idle_sword");
        AnimationShield("idle_shield");
    }

    void ToMoveArmed()
    {
        AnimationHero("run_armed");
        AnimationSword("run_sword");
        AnimationShield("run_shield");
    }

    void AnimationArmedAttack()
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
        float clipLength = TimeClip(startClip);
        Invoke("SetBusyAnimatorFalse", clipLength);
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

    void AnimationSword(string action)
    {
        Transform swordTransform = sword.transform;
        switch (orientation)
        {
            case "side_right":
                animator.Play($"{action}_side", 1);
                spriteRendererSword.flipX = false;
                swordTransform.position = new Vector3(swordTransform.position.x, swordTransform.position.y, -5);
                break;
            case "side_left":
                animator.Play($"{action}_side", 1);
                spriteRendererSword.flipX = true;
                swordTransform.position = new Vector3(swordTransform.position.x, swordTransform.position.y, -5);
                break;
            case "back":
                animator.Play($"{action}_back", 1);
                spriteRendererSword.flipX = false;
                swordTransform.position = new Vector3(swordTransform.position.x, swordTransform.position.y, -4);
                break;
            case "front":
                animator.Play($"{action}_front", 1);
                spriteRendererSword.flipX = false;
                swordTransform.position = new Vector3(swordTransform.position.x, swordTransform.position.y, -5);
                break;
        }
    }

    void AnimationShield(string action)
    {
        Transform shieldTransform = shield.transform;
        switch (orientation)
        {
            case "side_right":
                animator.Play($"{action}_side", 2);
                spriteRendererShield.flipX = false;
                shieldTransform.position = new Vector3(shieldTransform.position.x, shieldTransform.position.y, -5);
                break;
            case "side_left":
                animator.Play($"{action}_side", 2);
                spriteRendererShield.flipX = true;
                shieldTransform.position = new Vector3(shieldTransform.position.x, shieldTransform.position.y, -5);
                break;
            case "back":
                animator.Play($"{action}_back", 2);
                spriteRendererShield.flipX = false;
                shieldTransform.position = new Vector3(shieldTransform.position.x, shieldTransform.position.y, -5);
                break;
            case "front":
                animator.Play($"{action}_front", 2);
                spriteRendererShield.flipX = false;
                shieldTransform.position = new Vector3(shieldTransform.position.x, shieldTransform.position.y, -5);
                break;
        }
    }
}
