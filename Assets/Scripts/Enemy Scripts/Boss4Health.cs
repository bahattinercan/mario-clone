using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4Health : MonoBehaviour
{
    private Animator anim;
    private int health = 1;

    private bool canDamage;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        canDamage = true;
    }

    private IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canDamage)
        {
            Debug.Log(collision.gameObject.CompareTag(MyTags.BULLET));
            if (collision.gameObject.CompareTag(MyTags.BULLET))
            {
                health--;
                canDamage = false;
                if (health == 0)
                {
                    GetComponent<Boss4>().DeactivateBossScript();
                    anim.Play("Dead");
                }

                StartCoroutine(WaitForDamage());
            }
        }
    }
}