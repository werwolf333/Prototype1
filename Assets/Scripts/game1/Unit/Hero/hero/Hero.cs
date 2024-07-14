using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Hero : Unit
{
    public enum Options
    {
        walk,
        run,
        sprint
    }

    public Options optionsMoveHero;
    private string moveHero;
    
    private Rigidbody2D rb;
    public bool busyAnimator;
    private AllVision allVisionComponent;
    private Attack attackComponent;
    public bool isArmed;
    private GameObject sword;
    private GameObject shield;
    private SpriteRenderer spriteRendererSword;
    private SpriteRenderer spriteRendererShield;

    void OnValidate()
    {
        UpdateOptionString(optionsMoveHero);
    }

    public void SetOption(Options newOption)
    {
        UpdateOptionString(optionsMoveHero);
    }

    private void UpdateOptionString(Options option)
    {
        switch (option)
        {
            case Options.walk:
                moveHero = "walk";
                runningSpeed = 1f;
                break;
            case Options.run:
                moveHero = "run";
                runningSpeed = 2f;
                break;
            case Options.sprint:
                moveHero = "sprint";
                runningSpeed = 3f;
                break;
        }
    }

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
        transform.Translate(movement * runningSpeed * Time.deltaTime);
        ToOrientation(moveX, moveY);
        AnimationMoveHero(moveX, moveY);
    }

    void SetBusyAnimatorFalse()
    {
        busyAnimator = false;
    }
}
