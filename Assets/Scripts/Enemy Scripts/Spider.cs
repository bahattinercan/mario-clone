using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D myBody;

    public LayerMask playerLayer;
    public Transform topCollision;
    private Vector3 moveDirection = Vector3.down;
    public float speed = 1f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(ChangeMovement());
    }

    private void Update()
    {
        MoveSpider();
    }

    private void MoveSpider()
    {
        transform.Translate(moveDirection * speed * Time.smoothDeltaTime);
    }

    private IEnumerator ChangeMovement()
    {
        yield return new WaitForSeconds(3f);
        if (moveDirection == Vector3.down)
        {
            moveDirection = Vector3.up;
        }
        else
        {
            moveDirection = Vector3.down;
        }
        StartCoroutine(ChangeMovement());
    }

    private IEnumerator Dead()
    {
        anim.Play("Dead");
        myBody.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<BoxCollider2D>().isTrigger = true;
        StopCoroutine("ChangeMovement");
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void CheckForPlayerDamage()
    {
        Collider2D topHit = Physics2D.OverlapCircle(topCollision.position, .2f, playerLayer);

        if (topHit != null)
        {
            topHit.GetComponent<Rigidbody2D>().velocity =
                        new Vector2(topHit.GetComponent<Rigidbody2D>().velocity.x, 7f);
            StartCoroutine(Dead());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(MyTags.BULLET))
        {
            StartCoroutine(Dead());
        }
        else if (collision.CompareTag(MyTags.PLAYER))
        {
            collision.GetComponent<PlayerDamage>().DealDamage();
        }
    }
}