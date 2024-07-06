using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private Transform bar;
    private Transform cur;
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
            spriteRenderer.flipX = true;
        }
        if (orientation == "side_right")
        {
            startClip = "idle_side";
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
}
