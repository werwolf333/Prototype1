using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int id;
    public string orientation;
    public string type;
    public float health;
    public float stamina;
    public float equilibrium;
    public float mana;
    public float damage;
    public float protection;
    public float animationSpeed; //сомнительно, но окей
    public float attackSpeed;
    public float runningSpeed;
    public float level;

    protected string idle_back = "idle_back";
    protected string idle_front = "idle_front";
    protected string idle_side = "idle_side";

    protected string run_back = "run_back";
    protected string run_front = "run_front";
    protected string run_side = "run_side";

    protected string pain_back = "pain_back";
    protected string pain_front = "pain_front";
    protected string pain_side = "pain_side";

    protected string dying_back = "dying_back";
    protected string dying_front = "dying_front";
    protected string dying_side = "dying_side";

    protected string attack_back = "attack_back";
    protected string attack_front = "attack_front";
    protected string attack_side = "attack_side";

    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    protected Coroutine takeDamageCoroutine;

    protected float TimeClip(string clipName, Animator animator)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == clipName)
            {
                float playSpeed = animator.GetCurrentAnimatorStateInfo(0).speed;
                return clip.length / playSpeed;
            }
        }
        return 0f;
    }

    protected IEnumerator WaitAndPlayIdle(string startClip, string endClip)
    {
        animator.Play(startClip);
        float clipLength = TimeClip(startClip, animator);
        if (clipLength > 0)
        {
            yield return new WaitForSeconds(clipLength);
            animator.Play(endClip);
        }
    }

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
        return TimeClip(startClip, animator);
    }

    protected void ToDie()
    {
        Destroy(gameObject);
    }
}
