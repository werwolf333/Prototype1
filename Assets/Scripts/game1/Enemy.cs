using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform bar;
    private Transform cur;
    void Start()
    {
        bar = transform.Find("bar");
        cur = transform.Find("cur");
        bar.gameObject.SetActive(false);
        cur.gameObject.SetActive(false);
    }

    public void BarSetActive(bool b)
    {
        bar.gameObject.SetActive(b);
    }

    public void CurSetActive(bool b)
    {
        cur.gameObject.SetActive(b);
    }
}
