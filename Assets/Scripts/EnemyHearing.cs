using System.Collections.Generic;
using UnityEngine;

public class EnemyHearing : MonoBehaviour
{
    public static List<EnemyHearing> allEnemies = new List<EnemyHearing>();

    [Header("Hearing Settings")]
    public float hearingSensitivity = 5f;   // max distance this enemy can hear
    public float moveSpeed = 2f;            // how fast enemy moves toward sound

    private Vector3? lastHeardPosition = null;

    private void OnEnable()
    {
        allEnemies.Add(this);
    }

    private void OnDisable()
    {
        allEnemies.Remove(this);
    }

    public static void AlertEnemies(Vector3 footstepPosition, float radius)
    {
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
        Debug.Log($"Enemy heard a footstep at {footstepPosition}");
        lastHeardPosition = footstepPosition;
    }

    private void Update()
    {
        if (lastHeardPosition.HasValue)
        {
            // Move toward last heard position
            transform.position = Vector3.MoveTowards(transform.position, lastHeardPosition.Value, moveSpeed * Time.deltaTime
            );

            // Stop once reached
            if (Vector3.Distance(transform.position, lastHeardPosition.Value) < 0.1f)
            {
                lastHeardPosition = null;
            }
        }
    }
}
