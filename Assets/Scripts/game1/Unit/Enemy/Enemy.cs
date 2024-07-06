using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private Transform bar;
    private Transform cur;
    private Animator animator;
    private Coroutine takeDamageCoroutine;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        bar = transform.Find("bar");
        cur = transform.Find("cur");
        bar.gameObject.SetActive(false);
        cur.gameObject.SetActive(false);
        StartClip();
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
            var timeToDie = ToDie();
            Invoke("KillYourself", timeToDie); 
        }
        else
        {
            TakeDamage();
        }
    }

    void KillYourself()
    {
        Destroy(gameObject);
    }

    void StartClip()
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
        }
        if (orientation == "side_right")
        {
            startClip = "idle_side";
            spriteRenderer.flipX = true;
        }  
        animator.Play(startClip);  
    }

    public void BarSetActive(bool b)
    {
        bar.gameObject.SetActive(b);
    }

    public void CurSetActive(bool b)
    {
        cur.gameObject.SetActive(b);
    }

    public void TakeDamage()
    {
        var startClip = "";
        var endClip = "";
        if (orientation == "front")
        {
            startClip = "pain_front";
            endClip = "idle_front";
        }

        if (orientation == "back")
        {
            startClip = "pain_back";
            endClip = "idle_back";
        }

        if (orientation == "side_left")
        {
            startClip = "pain_side";
            endClip = "idle_side";
        }

        if (orientation == "side_right")
        {
            startClip = "pain_side";
            endClip = "idle_side";
            spriteRenderer.flipX = true;
        }
        if (takeDamageCoroutine != null)
        {
            StopCoroutine(takeDamageCoroutine);
        }
        takeDamageCoroutine = StartCoroutine(WaitAndPlayIdle(startClip, endClip, animator));
    }

    public float ToDie()
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
        }

        if (orientation == "side_right")
        {
            startClip = "dying_side";
            spriteRenderer.flipX = true;
        }
        animator.Play(startClip);
        return TimeClip(startClip, animator);
    }
}
