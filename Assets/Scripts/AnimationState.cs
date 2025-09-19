using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationState : MonoBehaviour
{   //input variables
    bool walkFoward = Input.GetKey(KeyCode.W);
    bool runFoward = Input.GetKey(KeyCode.LeftShift);
    bool walkBack = Input.GetKey(KeyCode.S);
    //reference animator parameters
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        //check if player is moving foward
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool walkFoward = Input.GetKey(KeyCode.W);
        //Check if player is running
        bool RunFoward = Input.GetKey(KeyCode.LeftShift);
        
        //if player is not walking and W is pressed, set isWalking to true and play walking animation
        if (!isWalking && walkFoward)
        {
            animator.SetBool(isWalkingHash, true);
        }
        //if player is walking and stops pressing W, set isWalking to false and stop walking animation
        
        if (isWalking && !walkFoward)
        {
            animator.SetBool(isWalkingHash, false);
        }
        //if player is not running and  W and LeftShift is pressed, set isRunning to true and play running animation
        if (  ( !walkFoward && RunFoward))
        {
            animator.SetBool(isRunningHash, true);
        }
        //if player is running and stops running or walking , set isRunning to false and stop running animation
        if ((!RunFoward && walkFoward))
        {
            animator.SetBool(isRunningHash, false);
        }
    }
}
