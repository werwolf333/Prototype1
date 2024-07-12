using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy : Unit
{
    public GameObject targetGoal;
    protected Transform bar;
    protected Transform cur;
    private Vector3[] patrolPoints;
    public int currentPointIndex = 0;
    protected Vector3 startPosition;

    private bool redy;
    public bool busyAnimator;

    protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        bar = transform.Find("bar");
        cur = transform.Find("cur");
        bar.gameObject.SetActive(false);
        cur.gameObject.SetActive(false);
        AnimationIdle();
        OnValidate();
        redy = true;
        UpdateOptionTactics(Options.patrol);
    }

    public void BarSetActive(bool b)
    {
        bar.gameObject.SetActive(b);
    }

    public void CurSetActive(bool b)
    {
        cur.gameObject.SetActive(b);
    }

    void Update()
    {
        if(!busyAnimator)
        {
            Attack();
        }
    }


protected void Attack()
{
    if (targetGoal != null)
    {
        Vector3 nextPoint = targetGoal.transform.position;
        nextPoint.z = nextPoint.y / 10;  // Устанавливаем Z в 10 раз меньше Y
        if (Vector3.Distance(transform.position, nextPoint) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPoint, runningSpeed * Time.deltaTime);
            Run(transform.position, nextPoint);
        }
    }
}

    void SetBusyAnimatorFalse()
    {
        busyAnimator = false;
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
}
