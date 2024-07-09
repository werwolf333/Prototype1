using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Hero : Unit
{
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
