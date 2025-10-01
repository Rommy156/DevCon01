using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    //audio variables
    public AudioSource footstepSource;
    public AudioClip[] walkFootsteps;
    public AudioClip[] runFootsteps;
    public float walkstepDistance = 0.4f;
    public float runstepDistance = 0.25f;
    
    private float stepTimer;

    bool isRunning = false;
    public float footstepVolume = 0.5f;
    public float hearingRadius = 5f;

    // Start is called before the first frame update
    void Start()
    {

        stepTimer = walkstepDistance;

        if (footstepSource == null)
            footstepSource = GetComponent<AudioSource>();

        if (footstepSource != null)
            footstepSource.loop = false;

        PlayFootstep();
    }

    // Update is called once per frame
    void Update()
    {
        // Check movement input
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);

        // Step interval depends on running/walking
        float stepInterval = isRunning ? runstepDistance : walkstepDistance;

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = stepInterval; // reset when idle
        }

    }
    void PlayFootstep()
    {
        if (footstepSource == null) return;

         AudioClip clipToPlay = isRunning ? runFootsteps[Random.Range(0, runFootsteps.Length)] : walkFootsteps[Random.Range(0, walkFootsteps.Length)];

        footstepSource.volume = isRunning ? footstepVolume * 1.5f : footstepVolume;
        footstepSource.PlayOneShot(clipToPlay);

        float radius = isRunning ? hearingRadius * 1.5f : hearingRadius;
        EnemyHearing.AlertEnemies(transform.position, radius);
    }
    }
