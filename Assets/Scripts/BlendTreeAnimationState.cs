using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class BlendTreeAnimationState : MonoBehaviour
{
    // Animator parameter hashes
    int velocityZHash;
    int velocityXHash;
    int isRunningHash;

    Rigidbody rb;
    Animator animator;

    // Current velocity values
    float VelocityZ = 0.0f;
    float VelocityX = 0.0f;

    // Movement settings
    public float acceleration = 0.05f;
    public float deceleration = 4.0f;
    public float maxWalkSpeed = 0.5f;
    public float maxRunSpeed = 1.0f;

    // World movement multiplier
    public float moveSpeedMultiplier = 0.25f;

    Vector2 inputVector;
    bool isRunning;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        velocityXHash = Animator.StringToHash("VelocityX");
        velocityZHash = Animator.StringToHash("VelocityZ");
        isRunningHash = Animator.StringToHash("isRunning");
    }
    private void FixedUpdate()
    {
        // Apply gravity manually
        rb.AddForce(Physics.gravity, ForceMode.Acceleration);
        //move position based on velocity
        rb.MovePosition(rb.position + new Vector3(VelocityX, 0, VelocityZ) * moveSpeedMultiplier * Time.fixedDeltaTime);
    }
    void Update()
        {
            // Input
            bool forward = Input.GetKey(KeyCode.W);
            bool backward = Input.GetKey(KeyCode.S);
            bool left = Input.GetKey(KeyCode.A);
            bool right = Input.GetKey(KeyCode.D);
            bool run = Input.GetKey(KeyCode.LeftShift);

            float currentMaxSpeed = run ? maxRunSpeed : maxWalkSpeed;
            // Target speeds
            float targetZ = 0f;
            if (forward) targetZ = currentMaxSpeed;
            if (backward) targetZ = -currentMaxSpeed;

            float targetX = 0f;
            if (left) targetX = -currentMaxSpeed;
            if (right) targetX = currentMaxSpeed;

            // Smooth accel/decel 
            VelocityZ = Mathf.MoveTowards(VelocityZ, targetZ, acceleration * Time.deltaTime);
            VelocityX = Mathf.MoveTowards(VelocityX, targetX, acceleration * Time.deltaTime);
            // Decelerate to zero if no input (this prevents sliding) 
            if (!forward && !backward) VelocityZ = Mathf.MoveTowards(VelocityZ, 0, acceleration * Time.deltaTime);
            if (!left && !right) VelocityX = Mathf.MoveTowards(VelocityX, 0, acceleration * Time.deltaTime);

            // Normalize diagonals
            Vector3 rawMove = new Vector3(VelocityX, 0f, VelocityZ);
            if (rawMove.magnitude > currentMaxSpeed)
                rawMove = rawMove.normalized * currentMaxSpeed;

            
            // Only rotate if we’re actually moving (avoid jitter when idle)
            if (rawMove.magnitude > 0.1f)
            {
                // Figure out the direction we should face
                Quaternion targetRotation = Quaternion.LookRotation(rawMove, Vector3.up);

                // Smoothly rotate towards that direction
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.deltaTime);
            }

            
            // Normalize for Animator (-1 to 1 range)
            float normX = rawMove.x / currentMaxSpeed;
            float normZ = rawMove.z / currentMaxSpeed;

            animator.SetFloat(velocityXHash, normX);
            animator.SetFloat(velocityZHash, normZ);
            animator.SetBool(isRunningHash, run);
        

        }
    }

