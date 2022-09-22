using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    private Animator anim;
    private bool animationStarted, animationFinished;
    private int jumpTimes;
    private bool jumpLeft = true;

    private string coroutineName = "FrogJump";

    public Transform topCollision;

    public LayerMask playerLayer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(FrogJump());
    }

    private void Update()
    {
        CheckForPlayerDamage();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (animationFinished && animationStarted)
        {
            animationStarted = false;
            Vector3 frogLocalPos = new Vector3(transform.localPosition.x * transform.parent.localScale.x, 0, 0);
            transform.parent.position += frogLocalPos;
            transform.localPosition = Vector3.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(MyTags.PLAYER))
        {
            collision.gameObject.GetComponent<PlayerDamage>().DealDamage();
        }
        else if (collision.gameObject.CompareTag(MyTags.BULLET))
        {
            Dead();
        }
    }

    private void CheckForPlayerDamage()
    {
        Collider2D topHit = Physics2D.OverlapCircle(topCollision.position, .2f, playerLayer);

        if (topHit != null)
        {
            topHit.GetComponent<Rigidbody2D>().velocity =
                        new Vector2(topHit.GetComponent<Rigidbody2D>().velocity.x, 7f);
            Dead();
        }
    }

    private void Dead()
    {
        StopCoroutine(coroutineName);
        gameObject.SetActive(false);
    }

    private IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        animationStarted = true;
        animationFinished = false;

        jumpTimes++;

        if (jumpLeft)
        {
            anim.Play("JumpLeft");
        }
        else
        {
            anim.Play("JumpRight");
        }

        StartCoroutine(coroutineName);
    }

    private void AnimationFinished()
    {
        animationFinished = true;

        if (jumpLeft)
        {
            anim.Play("IdleLeft");
        }
        else
        {
            anim.Play("IdleRight");
        }
        if (jumpTimes == 3)
        {
            jumpTimes = 0;
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1;
            transform.localScale = tempScale;

            jumpLeft = !jumpLeft;
        }
    }
}