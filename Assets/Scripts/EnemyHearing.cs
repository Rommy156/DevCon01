using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHearing : MonoBehaviour
{

    public static List<EnemyHearing> allEnemies = new List<EnemyHearing>();
    private static CaughtUIScript CaughtUIScript;


    // Enemy Hearing parameters
    // max distance this enemy can hear. how fast enemy moves toward sound
    public float hearingSensitivity = 5f;
    public float moveSpeed = 2f;
    public float radius = 10f;
    public AudioSource alertSound;
    // Last heard footstep position is null if none heard
    private Vector3? lastHeardPosition;
    public float caughtIncreaseRate = 5f;

    private void OnEnable()
    {
        // Register this enemy
        allEnemies.Add(this);

        //reference UI
        if (CaughtUIScript == null)
        {
            CaughtUIScript = GameObject.FindObjectOfType<CaughtUIScript>();
        }
    }

    private void OnDisable()
    {
        allEnemies.Remove(this);
    }

    public static void AlertEnemies(Vector3 footstepPosition, float radius)
    {
        // Notify all enemies within radius. for each enemy, check distance to footstepPosition.
        // If within radius and enemy's hearingSensitivity, call OnHearFootstep
        foreach (var enemy in allEnemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, footstepPosition);

            if (distance <= Mathf.Min(radius, enemy.hearingSensitivity))
            {
                enemy.OnHearFootstep(footstepPosition);
            }
        }
    }

    public void OnHearFootstep(Vector3 footstepPosition)
    {
        CaughtUIScript.playerVisible = false;
        Debug.Log($"Enemy heard a footstep at {footstepPosition}");
        lastHeardPosition = footstepPosition;

        // Play alert sound
        if (alertSound != null && !alertSound.isPlaying)
        {
            alertSound.Play();
        }
        // Update UI to indicate player is visible due to sound
        if (CaughtUIScript != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, footstepPosition);
            CaughtUIScript.caughtSlider.value += caughtIncreaseRate * Time.deltaTime;
            // Clamp caught value to max value and set playerVisible to true if max reached
            if (CaughtUIScript.caughtSlider.value >= CaughtUIScript.maxCaughtValue)
            {
                CaughtUIScript.playerVisible = true;
                CaughtUIScript.caughtSlider.value = CaughtUIScript.maxCaughtValue;
            }

        }
    }
    public void Update()
    {
        if (lastHeardPosition.HasValue)
        {
            //store last heard position value in target 
            Vector3 target = lastHeardPosition.Value;
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            //rotate toward last heard position
            Vector3 direction = (target - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
            if (Vector3.Distance(transform.position, target) < 1f)
            {
                lastHeardPosition = null;
                if (CaughtUIScript != null)
                {
                    CaughtUIScript.playerVisible = false;
                }
            }
        }

    }
}