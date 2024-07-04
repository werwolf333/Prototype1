using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform bar;
    private Transform cur;
    private Animator animator;
    public string orientation;
    void Start()
    {
        //orientation = "front";
        animator = GetComponent<Animator>();
        StartClip();
        bar = transform.Find("bar");
        cur = transform.Find("cur");
        bar.gameObject.SetActive(false);
        cur.gameObject.SetActive(false);
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

        if (orientation == "side")
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

        if (orientation == "side")
        {
            startClip = "pain_side";
            endClip = "idle_side";
        }


        animator.Play(startClip);
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

        if (orientation == "side")
        {
            startClip = "dying_side";
        }

        animator.Play(startClip);
        return TimeClip(startClip);
    }

    IEnumerator WaitAndPlayIdle(string startClip, string endClip)
    {
        float clipLength = TimeClip(startClip);
        if (clipLength > 0)
        {
            yield return new WaitForSeconds(clipLength );
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
