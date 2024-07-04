using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    private Sprite cidonia_idle_back;
    private Sprite cidonia_idle_front;
    private Sprite cidonia_idle_side;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        cidonia_idle_back = Resources.Load<Sprite>("hero/edle/cidonia_idle_back");
        cidonia_idle_front = Resources.Load<Sprite>("hero/edle/cidonia_idle_front");
        cidonia_idle_side = Resources.Load<Sprite>("hero/edle/cidonia_idle_side");
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        var hero = GetComponent<Unit>();
        moveSpeed = hero.runningSpeed;
        rb.gravityScale = 0;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        movement = new Vector2(moveX, moveY);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
        UpdateSpriteDirection(moveX, moveY);
    }

    void UpdateSpriteDirection(float moveX, float moveY)
    {
        if (Mathf.Abs(moveX) > Mathf.Abs(moveY))
        {
            spriteRenderer.sprite = cidonia_idle_side;
            if (moveX < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            if (moveY > 0)
            {
                spriteRenderer.sprite = cidonia_idle_back;
            }
            else
            {
                spriteRenderer.sprite = cidonia_idle_front;
            }
        }
    }
}
