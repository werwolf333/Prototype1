using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Hero : Unit
{
    private Rigidbody2D rb;
    public bool busyAnimator;
    private AllVision allVisionComponent;
    private Attack attackComponent;
    public bool isArmed;


    void Start()
    {
        PreStart();
        attackComponent = transform.Find("attack").GetComponent<Attack>();
        allVisionComponent = transform.Find("allVision").GetComponent<AllVision>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToAttack();
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        var movement = new Vector2(moveX, moveY);
        transform.Translate(movement * runningSpeed * Time.deltaTime);
        ToOrientation(moveX, moveY);
        AnimationMoveHero(moveX, moveY);
    }

    void ToAttack()
    {
        busyAnimator = true;
        if(isArmed)
        {
            AnimationArmedAttack();
            AnimationSwordAttack();
            AnimationShieldAttack();
        }
        else
        {
            AnimationUnarmedAttack();
        }
        var enemiesInAttack = attackComponent.enemiesInAttack;
        foreach (var enemy in enemiesInAttack)
        {
            var enemyComponent = enemy.GetComponent<Enemy>();
            enemyComponent.TakeDamage(damage);
        }
    }

    void SetBusyAnimatorFalse()
    {
        busyAnimator = false;
    }

    void AnimationUnarmedAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        var startClip = "";
        var endClip = "";

        if(orientation == "side_right")
        {
            startClip = "attack_side";
            endClip = "idle_side"; 
        }
        if(orientation == "side_left")
        {
            startClip = "attack_side";
            endClip = "idle_side"; 
        }
        if(orientation == "back")
        {
            startClip = "attack_back";
            endClip = "idle_back"; 
        }
        if(orientation == "front")
        {
            startClip = "attack_front";
            endClip = "idle_front"; 
        }
        attackCoroutine = StartCoroutine(WaitAndPlayIdle(startClip, endClip, 0));
        float clipLength = TimeClip(startClip);
        Invoke("SetBusyAnimatorFalse", clipLength);
    }
    
    void AnimationMoveHero(float moveX, float moveY)
    {
        if(!busyAnimator)
        {
            if(isArmed)
            {
                if(moveX == 0 && moveY == 0)
                {
                    ToStayArmed();
                }
                else
                {
                    ToMoveArmed();
                }
            }
            else
            {
                if(moveX == 0 && moveY == 0)
                {
                    ToStayUnarmed();
                }
                else
                {
                    ToMoveUnarmed();
                }
            }

        }
    }

    void  AnimationHero(string action)
    {
        switch (orientation)
        {
            case "side_right":
                animator.Play($"{action}_side"); 
                spriteRenderer.flipX = false;
                break;
            case "side_left":
                animator.Play($"{action}_side"); 
                spriteRenderer.flipX = true;
                break;
            case "back":
                animator.Play($"{action}_back"); 
                spriteRenderer.flipX = false;
                break;
            case "front":
                animator.Play($"{action}_front"); 
                spriteRenderer.flipX = false;
                break;
        }
    }

    void ToMoveUnarmed()
    {
        AnimationHero("run");
    }

    void ToStayUnarmed()
    {
        AnimationHero("idle");
    }

    void ToOrientation(float moveX, float moveY)
    {
        if (allVisionComponent.isTargetGoal && allVisionComponent.targetGoal != null)
        {
            NotFreeMove();
        }
        else
        {
            FreeMove(moveX, moveY);
        }
    }

    void NotFreeMove()
    {
        Transform target = allVisionComponent.targetGoal.transform;
        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        float targetX = direction.x;
        float targetY = direction.y;

        if (Mathf.Abs(targetX) > Mathf.Abs(targetY))
        {
            if (targetX > 0)
            {
                orientation = "side_right";
            }
            else
            {
                orientation = "side_left";
            }
        }
        else
        {
            if (targetY > 0)
            {
                orientation = "back";
            }
            else
            {
                orientation = "front";
            }
            }
    }

    void FreeMove(float moveX, float moveY)
    {
        if (moveX > 0)
        {
            orientation = "side_right";
        }
        else if (moveX < 0)
        {
            orientation = "side_left";
        }
        else if (moveY > 0)
        {
            orientation = "back";
        }
        else if (moveY < 0)
        {
            orientation = "front";
        } 
    }
}
