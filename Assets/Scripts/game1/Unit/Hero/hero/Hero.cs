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
    private GameObject sword;
    private GameObject shield;
    private SpriteRenderer spriteRendererSword;
    private SpriteRenderer spriteRendererShield;
    private string currentAnimation = "";
    private string currentClip = "";
    private float currentAnimationTime = 0f; 
    private string lastOrientation;

    void Start()
    {
        sword = transform.Find("sword").gameObject;
        shield = transform.Find("shield").gameObject;
        spriteRendererSword = sword.GetComponent<SpriteRenderer>();
        spriteRendererShield = shield.GetComponent<SpriteRenderer>();
        if(!isArmed)
        {
            sword.SetActive(false);
            shield.SetActive(false);
        }
        attackComponent = transform.Find("attack").GetComponent<Attack>();
        allVisionComponent = transform.Find("allVision").GetComponent<AllVision>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        StartCoroutine(UpdatePositionZ());
    }

    void UpdateOrientation()
    {
        if (lastOrientation != orientation)
        {
            lastOrientation = orientation;
            if(currentAnimation == "AnimationTakeDamageUnarmed")
            {
                AnimationTakeDamageUnarmed();
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ToCritAttack();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            TakeDamageHero();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ToAttack();
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        var movement = new Vector2(moveX, moveY);
        if(!isStopMove)
        {
            transform.Translate(movement * runningSpeed * Time.deltaTime);
        }
        ToOrientation(moveX, moveY);
        UpdateOrientation();
        AnimationMoveHero(moveX, moveY);
    }

    void SetBusyAnimatorFalse()
    {
        busyAnimator = false;
        currentAnimation = "";
        currentClip = "";
        currentAnimationTime = 0f;   
    }
}
