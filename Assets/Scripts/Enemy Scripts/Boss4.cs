using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4 : MonoBehaviour
{
    public GameObject stone;
    public Transform attackInstantiate;
    private Animator anim;
    private string coroutineName = "StartAttack";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(coroutineName);
    }

    private void Attack()
    {
        GameObject obj = Instantiate(stone, attackInstantiate.position, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300, -700), 0));
    }

    private void BackToIdle()
    {
        anim.Play("Idle");
    }

    public void DeactivateBossScript()
    {
        StopCoroutine(coroutineName);
        enabled = false;
    }

    private IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        anim.Play("Attack");
        StartCoroutine(coroutineName);
    }
}