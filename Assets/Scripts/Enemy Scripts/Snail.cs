using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float dragSpeed = 15f;
    private Rigidbody2D myBody;
    private Animator anim;
    private bool moveLeft;

    public LayerMask playerLayer;

    private bool canMove;
    private bool stunned;

    public Transform leftCollision, rightCollision, topCollision, downCollision;
    private Vector3 leftCollisionPos, rightCollisionPos;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        leftCollisionPos = leftCollision.localPosition;
        rightCollisionPos = rightCollision.localPosition;
    }

    private void Start()
    {
        moveLeft = true;
        canMove = true;
    }

    private void Update()
    {
        if (canMove)
        {
            if (moveLeft)
            {
                myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
            }
            else
            {
                myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
            }
        }

        CheckCollision();
    }

    private void CheckCollision()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftCollision.position, Vector2.down, .1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightCollision.position, Vector2.down, .1f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(topCollision.position, .2f, playerLayer);

        if (topHit != null)
        {
            if (topHit.CompareTag(MyTags.PLAYER))
            {
                if (!stunned)
                {
                    topHit.GetComponent<Rigidbody2D>().velocity =
                        new Vector2(topHit.GetComponent<Rigidbody2D>().velocity.x, 7f);

                    canMove = false;
                    myBody.velocity = Vector2.zero;
                    anim.Play("Stunned");
                    stunned = true;
                    if (CompareTag(MyTags.BEETLE))
                        StartCoroutine(Dead(.5f));
                }
                else
                {
                    topHit.GetComponent<Rigidbody2D>().velocity =
                       new Vector2(topHit.GetComponent<Rigidbody2D>().velocity.x, 7f);
                }
            }
        }

        if (leftHit)
        {
            if (leftHit.collider.gameObject.CompareTag(MyTags.PLAYER))
            {
                if (!stunned)
                {
                    // apply damage to the player
                    leftHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
                else
                {
                    if (!CompareTag(MyTags.BEETLE))
                    {
                        myBody.velocity = new Vector2(dragSpeed, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }

        if (rightHit)
        {
            if (rightHit.collider.gameObject.CompareTag(MyTags.PLAYER))
            {
                if (!stunned)
                {
                    Debug.Log("right damage");
                    rightHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
                else
                {
                    if (!CompareTag(MyTags.BEETLE))
                    {
                        myBody.velocity = new Vector2(-dragSpeed, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }

        if (!Physics2D.Raycast(downCollision.position, Vector2.down, .1f))
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        moveLeft = !moveLeft;

        Vector3 tempScale = transform.localScale;
        if (moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
            leftCollision.localPosition = leftCollisionPos;
            rightCollision.localPosition = rightCollisionPos;
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
            leftCollision.localPosition = rightCollisionPos;
            rightCollision.localPosition = leftCollisionPos;
        }
        transform.localScale = tempScale;
    }

    private IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(MyTags.BULLET))
        {
            if (CompareTag(MyTags.BEETLE))
            {
                anim.Play("Stunned");
                canMove = false;
                myBody.velocity = Vector2.zero;
                StartCoroutine(Dead(.4f));
            }
            if (CompareTag(MyTags.SNAIL))
            {
                if (!stunned)
                {
                    anim.Play("Stunned");
                    canMove = false;
                    myBody.velocity = Vector2.zero;
                }
                else
                {
                    StartCoroutine(Dead(.4f));
                }
            }
        }
    }
}