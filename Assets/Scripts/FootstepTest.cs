using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FootstepTest : MonoBehaviour
{
    public AudioSource source;     // assign in inspector
    public AudioClip footstepClip; // assign in inspector

    void Update()
    {
        // Press SPACE to play the footstep sound
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (source != null && footstepClip != null)
            {
                Debug.Log("Playing footstep test sound");
                source.PlayOneShot(footstepClip);
            }
            else
            {
                Debug.LogWarning("Missing AudioSource or AudioClip!");
            }
        }
    }
}
