using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : MonoBehaviour
{
    public Transform bottomCol;
    private Animator anim;
    public LayerMask playerLayer;

    private Vector3 moveDirection = Vector3.up;
    private Vector3 originPos, animPos;
    private bool startAnim;
    private bool canAnimate = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        originPos = transform.position;
        animPos = transform.position;
        animPos.y += .15f;
    }

    private void Update()
    {
        CheckForCollision();
        AnimateUpDown();
    }

    private void CheckForCollision()
    {
        if (canAnimate)
        {
            RaycastHit2D hit = Physics2D.Raycast(bottomCol.position, Vector2.down, .1f, playerLayer);

            if (hit)
            {
                if (hit.collider.CompareTag(MyTags.PLAYER))
                {
                    // increase the block
                    anim.Play("Idle");
                    startAnim = true;
                    canAnimate = false;
                }
            }
        }
    }

    private void AnimateUpDown()
    {
        if (startAnim)
        {
            transform.Translate(moveDirection * Time.smoothDeltaTime);

            if (transform.position.y >= animPos.y)
            {
                moveDirection = Vector3.down;
            }
            else if (transform.position.y <= originPos.y)
            {
                startAnim = false;
            }
        }
    }
}