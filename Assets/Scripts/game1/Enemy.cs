using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform bar;
    private Transform cur;
    private Animator animator;
    private Coroutine currentCoroutine;
    private SpriteRenderer spriteRenderer;
    public string orientation;
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
        var unit = GetComponent<Unit>();
        var pureAttack = attack - unit.protection;
        if(pureAttack<0)
        {
            pureAttack = 0;
        }
        unit.health = unit.health - pureAttack;
        if(unit.health <= 0)
        {
            var timeToDie = ToDie();
            Invoke("DestroyTargetGoalObject", timeToDie); 
        }
        else
        {
            TakeDamage();
        }
    }

    void DestroyTargetGoalObject()
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
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        StartCoroutine(WaitAndPlayIdle(startClip, endClip));
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
        return TimeClip(startClip);
    }

    IEnumerator WaitAndPlayIdle(string startClip, string endClip)
    {
        animator.Play(startClip);
        float clipLength = TimeClip(startClip);
        if (clipLength > 0)
        {
            yield return new WaitForSeconds(clipLength);
            animator.Play(endClip);
        }
    }

    float TimeClip(string clipName)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == clipName)
            {
                float playbackSpeed = animator.GetCurrentAnimatorStateInfo(0).speed;
                return clip.length / playbackSpeed;
            }
        }
        return 0f;
    }
}
