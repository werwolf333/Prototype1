using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private float attack = 2f;
    private GameObject targetGoalObject;
    public List<GameObject> enemiesInAttack = new List<GameObject>();
    public void RotateTo(Quaternion targetRotation, float rotationSpeed)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void  PutTargetGoalObject(GameObject curEnemy)
    {
        targetGoalObject = curEnemy;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            enemiesInAttack.Add(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            enemiesInAttack.Remove(collision.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (enemiesInAttack.Contains(targetGoalObject))
            {
                ToWound();
            }
        }
    }

    void ToWound()
    {
        var enemy = targetGoalObject.GetComponent<Enemy>();
        enemy.TakeDamage(attack);
    }
}
