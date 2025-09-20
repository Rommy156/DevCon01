using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyeballs : MonoBehaviour
{
    //mouse movement speed
    public float sensitivity = 3f;
    //for smooth out movement
    public float smoothFactor = 5f;
    //two to store calculations for us
    private Vector2 mouseLook;
    private Vector2 smoothMove;
    //refrence to the player
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        //the camera is the child of player so we can assign it easily.
        Player = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //make cursor invisible. hit ESC to get cursor back
        Cursor.lockState = CursorLockMode.Locked;

        //get the mouse input
        Vector2 mouseDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        //apply sensitivity 
        mouseDirection *= sensitivity;
        //smooth out the movement
        smoothMove = Vector2.Lerp(smoothMove, mouseDirection, Time.deltaTime * smoothFactor);
        //accumulate the mouse movement
        mouseLook += smoothMove;
        //clamp vertical movement to avoid flipping over
        mouseLook.y = Mathf.Clamp(mouseLook.y, -35f, 45f);
        // apply pitch to camera (X axis)
        transform.localRotation = Quaternion.Euler(-mouseLook.y, 0f, 0f);

        // apply yaw to player (Y axis)
        Player.transform.rotation = Quaternion.Euler(0f, mouseLook.x, 0f);
    }

    /*
    //temporary variable to store movement
    Vector2 mouseDirection = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    //scale sensitivity and smoothFactor variables
    mouseDirection.x *= sensitivity * smoothFactor;
    mouseDirection.y *= sensitivity * smoothFactor;
    //linear interpolation [lerp] between current position, calculated position at a speed of 1/ smoothFactor
    //(this is a cheap way of normalizing)
    smoothMove.x = Mathf.Lerp(smoothMove.x, mouseDirection.x, 1f / smoothFactor);
    smoothMove.y = Mathf.Lerp(smoothMove.y, mouseDirection.y, 1f / smoothFactor);
    //add those two calculations together
    mouseLook += smoothMove;
    //clamp to ensure mouse cant spin around in circle infinitely
    //Clamp parameters (what you're clamping, min,max) 
    mouseLook.y = Mathf.Clamp(mouseLook.y, -35f, 45f);
    //rotate camera on newly calculated position. player moves after
    transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
    //player moves on the x only
    Player.transform.rotation = Quaternion.AngleAxis(mouseLook.x, Player.transform.up);*/



}

