using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private void Start()
    {
        Invoke("Deactivate", 4f);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(MyTags.PLAYER))
        {
            // damage the player
            collision.gameObject.GetComponent<PlayerDamage>().DealDamage();
            CancelInvoke("Deactivate");
            gameObject.SetActive(false);
        }
    }
}