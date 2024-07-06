using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Unit
{
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public bool busyAnimator;


    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        movement = new Vector2(moveX, moveY);
        transform.Translate(movement * runningSpeed * Time.deltaTime);
        ToOrientation(moveX, moveY);
        AnimationHero(moveX, moveY);
    }
    
    void AnimationHero(float moveX, float moveY)
    {
        if(!busyAnimator)
        {
            if(moveX == 0 && moveY == 0)
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
            else
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
        }
    }

    void ToOrientation(float moveX, float moveY)
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
