using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(MyTags.PLAYER))
        {
            // damage the player
            collision.gameObject.GetComponent<PlayerDamage>().DealDamage();
        }
        gameObject.SetActive(false);
    }
}