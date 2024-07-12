using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy : Unit
{
    protected Coroutine coroutinePatrol;
    protected Coroutine moveToNextPointCoroutine;
    protected void ToPatrol()
    {
        if(coroutinePatrol == null)
        {
            startPosition = transform.position;
            float currentX = startPosition.x;
            float currentY = startPosition.y;
            patrolPoints = new Vector3[]
            {
                new Vector3(currentX + 2, currentY, 0),
                new Vector3(currentX + 2, currentY + 2, 0),
                new Vector3(currentX, currentY + 2, 0),
                new Vector3(currentX, currentY, 0)
            };
            coroutinePatrol = StartCoroutine(CoroutinePatrol());
        }
    }

    protected IEnumerator CoroutinePatrol()
    {
        while (true)
        {
            Vector3 nextPoint = patrolPoints[currentPointIndex];
            UpdateOrientation(transform.position, nextPoint);
            if (moveToNextPointCoroutine != null)
            {
                StopCoroutine(moveToNextPointCoroutine);
            }
            moveToNextPointCoroutine = StartCoroutine(MoveToNextPoint(nextPoint));
            yield return moveToNextPointCoroutine;
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    protected IEnumerator MoveToNextPoint(Vector3 nextPoint)
    {
        while (Vector3.Distance(transform.position, nextPoint) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPoint, runningSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = nextPoint;
    }
}
