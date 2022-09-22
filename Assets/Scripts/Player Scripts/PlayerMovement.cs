using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myBody;
    private Animator anim;
    private BoxCollider2D boxCollider2D;

    public float speed = 5f;
    public LayerMask groundLayer;
    public float jumpPower = 12f;
    private bool isGrounded, isJumped;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        CheckIfGrounded();
        PlayerJump();
    }

    private void FixedUpdate()
    {
        PlayerWalk();
    }

    private void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0)
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            ChangeDirection(1);
        }
        else if (h < 0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            ChangeDirection(-1);
        }
        else
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }
        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }

    private void PlayerJump()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                isJumped = true;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);

                anim.SetBool("Jump", true);
            }
        }
    }

    private void CheckIfGrounded()
    {
        isGrounded = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, groundLayer);
        if (isGrounded)
        {
            if (isJumped)
            {
                isJumped = false;
                anim.SetBool("Jump", false);
            }
        }
    }

    private void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }
}