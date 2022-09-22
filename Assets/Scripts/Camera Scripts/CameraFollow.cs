using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float resetSpeed = .5f;
    public float cameraSpeed = .3f;
    public Bounds cameraBounds;

    private Transform target;
    private float offsetZ;
    private Vector3 lastTargetPos;
    private Vector3 currentVelocity;
    private bool followsPlayer;

    private void Awake()
    {
        BoxCollider2D myCol = GetComponent<BoxCollider2D>();
        myCol.size = new Vector2(Camera.main.aspect * 2f * Camera.main.orthographicSize, 15f);
        cameraBounds = myCol.bounds;
    }

    // Start is called before the first frame update
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(MyTags.PLAYER).transform;
        lastTargetPos = target.position;
        offsetZ = (transform.position - target.position).z;
        followsPlayer = true;
    }

    private void FixedUpdate()
    {
        if (followsPlayer)
        {
            Vector3 aheadTargetPos = target.position + Vector3.forward * offsetZ;
            Vector3 newCameraPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, cameraSpeed);
            transform.position = new Vector3(newCameraPos.x, transform.position.y, newCameraPos.z);
            lastTargetPos = target.position;
        }
    }
}