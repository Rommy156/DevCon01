using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   //movement variables
    public float moveSpeed = 7f;
    public float strafeSpeed = 4f;

    //create a vector3 to store movement calculations
    private Vector3 movement;
    private Rigidbody rb;

    //vector3 to store jump direction
    private Vector3 jumpDirection;
    //float variable to store jump height (y)
    public float jumpHeight = 4f;
    //bool variablle to check if it's grounded
    public bool isGrounded;

    public LayerMask groundLayer;


    // Start is called before the first frame update
    void Start()
    {
        //initialize rigidBody component
        rb = GetComponent<Rigidbody>();

        //set initial jump direction
        //jumpDirection = new Vector3(0, jumpHeight, 0);
        jumpDirection = Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {   //check the ground using a function
        isGrounded = CheckGround();
        //now that we know the answer to isGrounded, call the Jump() function.
        if (isGrounded)
        {
            Jump();
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //add force to rigidbody inn the direction of jumpDirection * height
            //ForceMode.Impulse means "the frame it is called" or immediately
            rb.AddForce(jumpDirection * jumpHeight, ForceMode.Impulse);

        }
    }
    //FixedUpdate is called per frame at a set interval
    private void FixedUpdate()
    {
        //create temporary floates to store Horizontal and Vertical input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //calculate variable, then set temporary vector to whatever transform.forward is (z-axis)
        //multiplied by the (v) verticacl axis (-1 ,0 ,1 ) multiplied ny the move speed
        //then add it by transform.right(x-axis) by (-1 ,0 ,1 ) and strafeSpeed, which is slower

        Vector3.Normalize(movement);
        movement = (transform.forward * v * moveSpeed) + (transform.right * h * strafeSpeed);

        //move with the RigidBody
        //your current position + answer to the calculation above and muliplied with Time.DeltaTime
        rb.MovePosition(transform.position + movement * Time.deltaTime);
    }

    bool CheckGround()
    {
        //raycastHit is a variable that represents the actual collision
        RaycastHit hit;
        //checking if the raycast hits the ground layer


        if (Physics.Raycast(transform.position, Vector3.down, out hit, 3f, groundLayer))
        {
            return true;
        }
        return false;
    }

}


