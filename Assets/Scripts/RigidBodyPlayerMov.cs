using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyPlayerMov : MonoBehaviour
{
    public float speed = 6f;
    public float turnSpeed = 60f;
    Rigidbody rb;
    public float smoothMoveTime;
    float smoothInputMagnitude;
    float smoothMoveVelocity;
    float angle;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //input direction
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        float inputMagnitude = Mathf.Clamp01(inputDirection.magnitude);

        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);

        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        angle = Mathf.MoveTowardsAngle(angle, targetAngle, turnSpeed * Time.deltaTime * inputMagnitude);

        velocity = transform.forward * speed * smoothInputMagnitude;
    }
    private void FixedUpdate()
    {
        rb.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        Vector3 moveDirection = transform.forward * smoothInputMagnitude * speed * Time.deltaTime;
        rb.MovePosition(rb.position + moveDirection);
    }
}
