using System.Collections;
using UnityEngine;

public partial class Enemy : Unit
{
    private bool canAttack;
    private Coroutine attackCoroutine;
    private bool isAttack;
    //private bool canAttruetack = true;

    void Attack()
    {
        if (isAttack)
        {
            return;
        }
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        var clipLength = AnimationAttack();
        attackCoroutine = StartCoroutine(AttackCoroutine(clipLength));
    }

    private IEnumerator AttackCoroutine(float clipLength)
    {
        isAttack = true;
        yield return new WaitForSeconds(0.66f);
        var heroComponent = targetGoal.GetComponent<Hero>();
        heroComponent.TakeDamageHero();
        yield return new WaitForSeconds(clipLength -0.66f);
        isAttack = false;
    }


    /*void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "hero")
        {
            canAttack = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "hero")
        {
            canAttack = false;
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }*/ 

    protected float AnimationAttack()
    {
        var startClip = "";
        if (orientation == "front")
        {
            startClip = "attack_front";
            spriteRenderer.flipX = false;
        }

        if (orientation == "back")
        {
            startClip = "attack_back";
            spriteRenderer.flipX = false;
        }

        if (orientation == "side_left")
        {
            startClip = "attack_side";
            spriteRenderer.flipX = false;
        }
        if (orientation == "side_right")
        {
            startClip = "attack_side";
            spriteRenderer.flipX = true;
        }  
        float clipLength = TimeClip(startClip);
        animator.Play(startClip, 0, 0f); 
        return clipLength;
    }
}
