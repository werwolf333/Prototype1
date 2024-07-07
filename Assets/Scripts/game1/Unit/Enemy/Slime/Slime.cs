using System.Collections;
using UnityEngine;

public class Slime : Enemy
{
    public string tactics;
    private Vector3[] patrolPoints;
    public int currentPointIndex = 0;

    protected void Start()
    {
        PreStart();
        if(tactics == "patrol")
        {
            ToPatrol();
        }
    }

    void ToPatrol()
    {
        Vector3 currentPosition = transform.position;
        float currentX = currentPosition.x;
        float currentY = currentPosition.y;
        patrolPoints = new Vector3[]
        {
            new Vector3(currentX + 2, currentY, 0),
            new Vector3(currentX + 2, currentY + 2, 0),
            new Vector3(currentX, currentY + 2, 0),
            new Vector3(currentX, currentY, 0)
        };
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            Vector3 nextPoint = patrolPoints[currentPointIndex];
            UpdateOrientation(transform.position, nextPoint);
            yield return StartCoroutine(MoveToNextPoint(nextPoint));
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    public IEnumerator MoveToNextPoint(Vector3 nextPoint)
    {
        while (Vector3.Distance(transform.position, nextPoint) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPoint, runningSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = nextPoint;
    }

    private void UpdateOrientation(Vector3 currentPosition, Vector3 nextPoint)
    {
        Vector3 direction = nextPoint - currentPosition;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                orientation = "side_right";
                spriteRenderer.flipX = false;
            }
            else
            {
                orientation = "side_left";
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                orientation = "back";
                spriteRenderer.flipX = false;
            }
            else
            {
                orientation = "front";
                spriteRenderer.flipX = false;
            }
        }
        AnimationRun();
    }
}
