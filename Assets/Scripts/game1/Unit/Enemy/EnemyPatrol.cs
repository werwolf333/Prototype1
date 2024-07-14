using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy : Unit
{
    private Vector2[] patrolPoints;
    public int currentPointIndex = 0;
    protected Coroutine coroutinePatrol;
    protected Coroutine moveToNextPointCoroutine;
    protected void ToPatrol()
    {
        if(coroutinePatrol == null)
        {
            startPosition = transform.position;
            float currentX = startPosition.x;
            float currentY = startPosition.y;
            patrolPoints = new Vector2[]
            {
                new Vector2(currentX + 2, currentY),
                new Vector2(currentX + 2, currentY + 2),
                new Vector2(currentX, currentY + 2),
                new Vector2(currentX, currentY)
            };
            coroutinePatrol = StartCoroutine(CoroutinePatrol());
        }
    }

    protected IEnumerator CoroutinePatrol()
    {
        while (true)
        {
            Vector3 nextPoint = patrolPoints[currentPointIndex];
            Run(transform.position, nextPoint);
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
