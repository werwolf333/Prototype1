using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{    public List<GameObject> enemiesInAttack = new List<GameObject>();

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

    public void RotateTo(Quaternion targetRotation, float rotationSpeed)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public bool  TargetCheck(GameObject curEnemy)
    {
        if (enemiesInAttack.Contains(curEnemy))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
