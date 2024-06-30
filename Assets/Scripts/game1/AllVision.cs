using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllVision : MonoBehaviour
{
    public List<GameObject> enemiesInAllView = new List<GameObject>();
    private int targetGoal = 0;
    public bool isTargetGoal;
    private Vision visionComponent;
    private GameObject vision;

    void Start()
    {
        visionComponent = GameObject.Find("vision").GetComponent<Vision>();
        vision = GameObject.Find("vision");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "enemy")
        {
            enemiesInAllView.Add(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "enemy")
        {
            enemiesInAllView.Remove(collision.gameObject);
        }
    }

    void FindTarget()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isTargetGoal = true;
            targetGoal++;
            if(targetGoal >= enemiesInAllView.Count)
            {
                targetGoal = 0;
            }

            foreach (var enemyView in enemiesInAllView)
            {
                var enemy = enemyView.GetComponent<Enemy>();
                enemy.CurSetActive(false);
            }

            var targetEnemy  = enemiesInAllView[targetGoal].GetComponent<Enemy>();
            targetEnemy .CurSetActive(true);
        }
    }

    void Update()
    {
        if (enemiesInAllView.Count == 0)
        {
            isTargetGoal = false;
        }
        else
        {
            SortEnemiesByDistance();
            FindTarget();
        }

        if (isTargetGoal)
        {
            ToTarget();
        }
        else
        {
            ToNotTarget();
        }
    }

    void ToTarget()
    {
        if (targetGoal >= 0 && targetGoal < enemiesInAllView.Count && enemiesInAllView[targetGoal] != null)
        {
            var rotationSpeed = 20f;
            Vector2 directionToTarget = (enemiesInAllView[targetGoal].transform.position - vision.transform.position).normalized;
            float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
            float adjustedAngle = targetAngle + 180f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, adjustedAngle));
            visionComponent.RotateTo(targetRotation, rotationSpeed);
        }
    }

    void ToNotTarget()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        var movement = new Vector2(moveX, moveY);
        var rotationSpeed = 5f;
        if (movement != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg + 180;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, targetAngle));
            visionComponent.RotateTo(targetRotation, rotationSpeed);
        }
    }

    void SortEnemiesByDistance()
    {
        enemiesInAllView.Sort((enemy1, enemy2) =>
        {
            float distanceToEnemy1 = Vector3.Distance(transform.position, enemy1.transform.position);
            float distanceToEnemy2 = Vector3.Distance(transform.position, enemy2.transform.position);
            return distanceToEnemy1.CompareTo(distanceToEnemy2);
        });
    }
}
