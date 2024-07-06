using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Unit
{
    private Rigidbody2D rb;
    public bool busyAnimator;
    private AllVision allVisionComponent;


    void Start()
    {
        allVisionComponent = GameObject.Find("allVision").GetComponent<AllVision>();
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
        if(allVisionComponent.attackable)
        {
            busyAnimator = true;
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
            attackCoroutine = StartCoroutine(WaitAndPlayIdle(startClip, endClip));
            var enemy = allVisionComponent.targetGoal.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            float clipLength = TimeClip(startClip);
            Invoke("SetBusyAnimatorFalse", clipLength);
        }  
    }

    void SetBusyAnimatorFalse()
    {
        busyAnimator = false;
    }
    
    void AnimationMoveHero(float moveX, float moveY)
    {
        if(!busyAnimator)
        {
            if(moveX == 0 && moveY == 0)
            {
                ToStay();
            }
            else
            {
                ToMove();
            }
        }
    }

    void ToMove()
    {
        if(orientation == "side_right")
        {
            animator.Play("run_side"); 
            spriteRenderer.flipX = false;
        }
        if(orientation == "side_left")
        {
            animator.Play("run_side"); 
            spriteRenderer.flipX = true;
        }
        if(orientation == "back")
        {
            animator.Play("run_back"); 
            spriteRenderer.flipX = false;
        }
        if(orientation == "front")
        {
            animator.Play("run_front"); 
            spriteRenderer.flipX = false;
        }
    }

    void ToStay()
    {
        if(orientation == "side_right")
        {
            animator.Play("idle_side"); 
            spriteRenderer.flipX = false;
        }
        if(orientation == "side_left")
        {
            animator.Play("idle_side"); 
            spriteRenderer.flipX = true;
        }
        if(orientation == "back")
        {
            animator.Play("idle_back"); 
            spriteRenderer.flipX = false;
        }
        if(orientation == "front")
        {
            animator.Play("idle_front"); 
            spriteRenderer.flipX = false;
        }
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
