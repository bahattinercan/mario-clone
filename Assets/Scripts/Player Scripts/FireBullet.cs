using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private float speed = 10f;
    private Animator anim;
    private bool canMove;

    public float Speed { get => speed; set => speed = value; }

    private void Awake()
    {
        canMove = true;
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(DisableBullet(5f));
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
        }
    }

    private IEnumerator DisableBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (
            collision.CompareTag(MyTags.BEETLE) ||
            collision.CompareTag(MyTags.SNAIL) ||
            collision.CompareTag(MyTags.BIRD) ||
            collision.CompareTag(MyTags.SPIDER) ||
            collision.CompareTag(MyTags.FROG) ||
            collision.CompareTag(MyTags.BOSS4) ||
            collision.CompareTag(MyTags.BONUSBLOCK) ||
            collision.CompareTag(MyTags.BREAKABLEBLOCK)
            )
        {
            StopCoroutine("DisableBullet");
            transform.localScale *= 3;
            anim.Play("Explotion");
            canMove = false;
            StartCoroutine(DisableBullet(.15f));
        }
    }
}