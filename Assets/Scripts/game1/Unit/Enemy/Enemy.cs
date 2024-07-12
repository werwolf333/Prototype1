using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy : Unit
{
    public enum Options
    {
        patrol,
        attack,
        comeback,
        wait
    }
    public Options optionsTactics;
    protected string curTactics;
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

    private void UpdateOptionTactics(Options option)
    {
        optionsTactics = option;
        UpdateOption(option);
    }

    void OnValidate()
    {
        UpdateOption(optionsTactics);
    }

    private void UpdateOption(Options option)
    {
        if(redy)
        {
            if (coroutinePatrol != null)
            {
                StopCoroutine(coroutinePatrol);
                StopCoroutine(moveToNextPointCoroutine);
                coroutinePatrol = null;
            }
            switch (option)
            {
                case Options.patrol:
                if(curTactics != "patrol")
                {
                    ToPatrol();
                }
                    curTactics = "patrol";
                    break;
                case Options.attack:
                    curTactics = "attack";
                    break;
                case Options.comeback:
                    curTactics = "comeback";
                    break;
                case Options.wait:
                    curTactics = "wait";
                    break;
            }
        }
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
            UpdateOrientation(transform.position, nextPoint);
        }
    }
}

    protected void UpdateOrientation(Vector3 currentPosition, Vector3 nextPoint)
    {
        Vector3 direction = nextPoint - currentPosition;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                orientation = "side_right";
                spriteRenderer.flipX = false;
            }
            else
            {
                orientation = "side_left";
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                orientation = "back";
                spriteRenderer.flipX = false;
            }
            else
            {
                orientation = "front";
                spriteRenderer.flipX = false;
            }
        }
        AnimationRun();
    }

    void SetBusyAnimatorFalse()
    {
        busyAnimator = false;
    }
}
