using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Hero : Unit
{
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
                    if(moveHero == "walk")
                    {
                        ToWalkArmed();
                    }
                    if(moveHero == "run")
                    {
                        ToRunArmed();
                    }
                    if(moveHero == "sprint")
                    {
                        ToSprintArmed();
                    }
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
                    if(moveHero == "walk")
                    {
                        ToWalkUnarmed();
                    }
                    if(moveHero == "run")
                    {
                        ToRunUnarmed();
                    }
                    if(moveHero == "sprint")
                    {
                        ToSprintUnarmed();
                    }
                }
            }
        }
    }

    void ToSprintArmed()
    {
        AnimationHero("sprint_armed");
        AnimationSword("sprint_sword");
        AnimationShield("sprint_shield");
    }

    void ToSprintUnarmed()
    {
        AnimationHero("sprint");
    }

    void ToWalkArmed()
    {
        AnimationHero("walk_armed");
        AnimationSword("walk_sword");
        AnimationShield("walk_shield");
    }

    void ToWalkUnarmed()
    {
        AnimationHero("walk");
    }
    void ToRunUnarmed()
    {
        AnimationHero("run");
    }

    void ToStayUnarmed()
    {
        AnimationHero("idle");
    }

    void ToStayArmed()
    {
        AnimationHero("idle_armed");
        AnimationSword("idle_sword");
        AnimationShield("idle_shield");
    }

    void ToRunArmed()
    {
        AnimationHero("run_armed");
        AnimationSword("run_sword");
        AnimationShield("run_shield");
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

    void AnimationSword(string action)
    {
        Transform swordTransform = sword.transform;
        switch (orientation)
        {
            case "side_right":
                animator.Play($"{action}_side", 1);
                spriteRendererSword.flipX = false;
                swordTransform.position = new Vector3(swordTransform.position.x, swordTransform.position.y, -5);
                break;
            case "side_left":
                animator.Play($"{action}_side", 1);
                spriteRendererSword.flipX = true;
                swordTransform.position = new Vector3(swordTransform.position.x, swordTransform.position.y, -5);
                break;
            case "back":
                animator.Play($"{action}_back", 1);
                spriteRendererSword.flipX = false;
                swordTransform.position = new Vector3(swordTransform.position.x, swordTransform.position.y, -4);
                break;
            case "front":
                animator.Play($"{action}_front", 1);
                spriteRendererSword.flipX = false;
                swordTransform.position = new Vector3(swordTransform.position.x, swordTransform.position.y, -6);
                break;
        }
    }

    void AnimationShield(string action)
    {
        Transform shieldTransform = shield.transform;
        switch (orientation)
        {
            case "side_right":
                animator.Play($"{action}_side", 2);
                spriteRendererShield.flipX = false;
                shieldTransform.position = new Vector3(shieldTransform.position.x, shieldTransform.position.y, -5);
                break;
            case "side_left":
                animator.Play($"{action}_side", 2);
                spriteRendererShield.flipX = true;
                shieldTransform.position = new Vector3(shieldTransform.position.x, shieldTransform.position.y, -5);
                break;
            case "back":
                animator.Play($"{action}_back", 2);
                spriteRendererShield.flipX = false;
                shieldTransform.position = new Vector3(shieldTransform.position.x, shieldTransform.position.y, -5);
                break;
            case "front":
                animator.Play($"{action}_front", 2);
                spriteRendererShield.flipX = false;
                shieldTransform.position = new Vector3(shieldTransform.position.x, shieldTransform.position.y, -5);
                break;
        }
    }
}
