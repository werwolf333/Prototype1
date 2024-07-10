using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit : MonoBehaviour
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

    protected Animator animator;
    protected SpriteRenderer spriteRenderer;


    protected float TimeClip(string clipName)
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

    protected void AnimationIdle()
    {
        var startClip = "";
        if (orientation == "front")
        {
            startClip = "idle_front";
        }

        if (orientation == "back")
        {
            startClip = "idle_back";
        }

        if (orientation == "side_left")
        {
            startClip = "idle_side";
            spriteRenderer.flipX = true;
        }
        if (orientation == "side_right")
        {
            startClip = "idle_side";
        }  
        animator.Play(startClip);  
    }

    protected void AnimationRun()
    {
        var startClip = "";
        if (orientation == "front")
        {
            startClip = "run_front";
        }

        if (orientation == "back")
        {
            startClip = "run_back";
        }

        if (orientation == "side_left")
        {
            startClip = "run_side";
            spriteRenderer.flipX = true;
        }
        if (orientation == "side_right")
        {
            startClip = "run_side";
        }  
        animator.Play(startClip);  
    } 
}
