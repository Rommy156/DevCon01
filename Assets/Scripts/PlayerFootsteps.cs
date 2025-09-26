using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource footstepSource;
    public AudioClip footstep;
    public float walkstepDistance = 0.4f;
    public float runstepDistance = 0.25f;

    private AudioSource audioSource;
    private float stepTimer;
    private Animator animator;

    bool isRunning = false;
    public float footstepVolume = 0.5f;
    public float hearingRadius = 5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        stepTimer = walkstepDistance;
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if running (Shift + W/A/S/D)
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);

        if (isRunning)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                footstepSource.PlayOneShot(footstep);
                stepTimer = footstepVolume;
            }
        }
        else
        {
            stepTimer = footstepVolume; // reset when not running
        }
        void PlayFootstep()
        {
            {
                audioSource.clip = footstep;
                audioSource.volume = isRunning ? footstepVolume * 1.5f : footstepVolume;
                audioSource.Play();

                float radius = isRunning ? hearingRadius * 1.5f : hearingRadius;
                EnemyHearing.AlertEnemies(transform.position, radius);
            }

        }
        if (isMoving && animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = runstepDistance;
            }
        }
        else if (isMoving && animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = walkstepDistance;
            }
        }
        else
        {
            stepTimer = 0f; // reset when not moving
        }
    }
    void PlayFootstep()
    {
        {
            audioSource.clip = footstep;
            audioSource.volume = isRunning ? footstepVolume * 1.5f : footstepVolume;
            audioSource.Play();
            float radius = isRunning ? hearingRadius * 1.5f : hearingRadius;
            EnemyHearing.AlertEnemies(transform.position, radius);
        }
    }

}
