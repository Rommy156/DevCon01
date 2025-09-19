using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlendtreeAnimationState : MonoBehaviour
{
    //reference animator parameters
    Animator animator;
    float velocity = 0.0f;
    public float acceleration = 2f;
    public float deceleration = 2f;
    int velocityHash;
    // Start is called before the first frame update
    void Start()
    {
        //add reference to animator component
         animator = GetComponent<Animator>();
        //better way to reference velocity parameter in animator 
        velocityHash = Animator.StringToHash("velocity");
    }

    // Update is called once per frame
    void Update()
    {
        //input variables
        bool walkFoward = Input.GetKey(KeyCode.W);
        bool runFoward = Input.GetKey(KeyCode.LeftShift);
        bool walkBack = Input.GetKey(KeyCode.S);
        //add time.deltaTime to velocity
        if (walkFoward && velocity < 5)
        { velocity += Time.deltaTime; }
        //decrease velocity if not walking foward. 
        if (!walkFoward && velocity > 0)
        { velocity -= Time.deltaTime * acceleration; }
        if (!walkFoward && velocity <= 0)
        {
            velocity = 0f;
        }
        //clamp velocity between 0 and 1
        velocity = Mathf.Clamp(velocity, -1f, 1f);
        //set velocity parameter in animator
        animator.SetFloat(velocityHash, velocity);

        


    }


}
