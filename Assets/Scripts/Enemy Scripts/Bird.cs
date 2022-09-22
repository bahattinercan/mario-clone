using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D myBody;
    private Animator anim;

    private Vector3 moveDirection = Vector3.left;
    private Vector3 originPos, movePos;

    public GameObject birdEgg;
    public LayerMask playerLayer;
    private bool attacked, canMove;

    public float speed = 2.5f, flyDistance = 3f;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        originPos = transform.position;
        originPos.x += flyDistance / 2;

        movePos = transform.position;
        movePos.x -= flyDistance / 2;

        canMove = true;
    }

    private void Update()
    {
        MoveTheBird();
        DropEgg();
    }

    private void MoveTheBird()
    {
        if (canMove)
        {
            transform.Translate(moveDirection * speed * Time.smoothDeltaTime);

            if (transform.position.x >= originPos.x)
            {
                moveDirection = Vector3.left;
                ChangeDirection(.5f);
            }
            else if (transform.position.x <= movePos.x)
            {
                moveDirection = Vector3.right;
                ChangeDirection(-.5f);
            }
        }
    }

    private void ChangeDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    private void DropEgg()
    {
        if (!attacked)
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer))
            {
                attacked = true;
                Vector3 eggSpawnPos = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
                Instantiate(birdEgg, eggSpawnPos, Quaternion.identity);
                anim.Play("Fly");
            }
        }
    }

    private IEnumerator BirdDead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(MyTags.BULLET))
        {
            anim.Play("Dead");
            GetComponent<BoxCollider2D>().isTrigger = true;
            myBody.bodyType = RigidbodyType2D.Dynamic;
            canMove = false;
            StartCoroutine(BirdDead(3f));
        }
    }
}