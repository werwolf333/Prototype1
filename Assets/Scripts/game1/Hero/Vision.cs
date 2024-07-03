using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public void RotateTo(Quaternion targetRotation, float rotationSpeed)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            var enemyComponent = collision.gameObject.GetComponent<Enemy>();
            enemyComponent.BarSetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            var enemyComponent = collision.gameObject.GetComponent<Enemy>();
            enemyComponent.BarSetActive(false);
        }
    }
}
