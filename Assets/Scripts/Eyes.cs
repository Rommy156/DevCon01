using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;

    //range of camera movement
    public float smoothSpeed = 0.5f;
    void Start()
    {
        offset = transform.position - player.position;
    }
    private void Update()
    {
        Vector3 newPos = player.position + offset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothSpeed * Time.deltaTime);
    }
}

