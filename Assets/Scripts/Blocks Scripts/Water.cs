using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private Vector3 startPoint;

    private void Start()
    {
        startPoint = GameObject.FindGameObjectWithTag(MyTags.STARTPOINT).transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(MyTags.PLAYER))
        {
            collision.gameObject.GetComponent<PlayerDamage>().DealDamage();
            collision.transform.position = startPoint;
        }
    }
}