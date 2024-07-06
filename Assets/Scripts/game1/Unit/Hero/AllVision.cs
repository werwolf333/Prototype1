using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllVision : MonoBehaviour
{
    public List<GameObject> enemiesInAllView = new List<GameObject>();
    public bool isTargetGoal;
    public bool attackable;
    public GameObject targetGoal;
    private Vision visionComponent;
    private GameObject vision;
    private Attack attackComponent;

    void Start()
    {
        visionComponent = GameObject.Find("vision").GetComponent<Vision>();
        vision = GameObject.Find("vision");
        attackComponent = GameObject.Find("attack").GetComponent<Attack>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            enemiesInAllView.Add(collision.gameObject);
            SortEnemiesByDistance();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            var targetEnemy = collision.gameObject.GetComponent<Enemy>();
            targetEnemy.CurSetActive(false);
            enemiesInAllView.Remove(collision.gameObject);
            SortEnemiesByDistance();
        }
    }

    void FindTarget()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(!isTargetGoal)
            {
                targetGoal = enemiesInAllView[0];
            }
            else
            {
                SelectNextTarget();
            }
            isTargetGoal = true;
        }
    }

    void SelectNextTarget()
    {
        int index = enemiesInAllView.IndexOf(targetGoal); 
        if(index + 1 >= enemiesInAllView.Count)
        {
            targetGoal = enemiesInAllView[0];
        }
        else
        {
            targetGoal = enemiesInAllView[index + 1];
        }
    }

    void ShowCurTarget()
    {
        if(enemiesInAllView.Count != 0)
        {
            foreach (var enemyView in enemiesInAllView)
            {
                var enemy = enemyView.GetComponent<Enemy>();
                enemy.CurSetActive(false);
            }
            var targetEnemy  = targetGoal.GetComponent<Enemy>();
            targetEnemy.CurSetActive(true);
            attackable = attackComponent.TargetCheck(targetGoal);
        }
        else
        {
            foreach (var enemyView in enemiesInAllView)
            {
                var enemy = enemyView.GetComponent<Enemy>();
                enemy.CurSetActive(false);
            }
        }
    }

    void Update()
    {
        if (enemiesInAllView.Count == 0)
        {
            isTargetGoal = false;
            attackable = false;
            attackable = false;
            //attackComponent.TargetCheck(null);
        }
        else
        {
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
        GameObject goalObject = null;
        if(enemiesInAllView.Contains(targetGoal))
        {
            goalObject = targetGoal;
        }
        else
        {
            SelectNextTarget();
        }
        var rotationSpeed = 20f;
        if(goalObject != null)
        {
            Vector2 directionToTarget = (goalObject.transform.position - vision.transform.position).normalized;
            float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
            float adjustedAngle = targetAngle + 180f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, adjustedAngle));
            visionComponent.RotateTo(targetRotation, rotationSpeed);
            attackComponent.RotateTo(targetRotation, rotationSpeed);       
            ShowCurTarget();
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
            attackComponent.RotateTo(targetRotation, rotationSpeed); 
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
