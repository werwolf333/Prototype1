using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform bar;
    private Transform cur;
    private Animator animator;
    private string orientation;
    void Start()
    {
        animator = GetComponent<Animator>();
        bar = transform.Find("bar");
        cur = transform.Find("cur");
        bar.gameObject.SetActive(false);
        cur.gameObject.SetActive(false);
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
        var startClip = "pain_front";
        var endClip = "idle_front";
        animator.Play(startClip);
        StartCoroutine(WaitAndPlayIdle(startClip, endClip));
    }

    public float ToDie()
    {
        var startClip = "dying_front";
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
