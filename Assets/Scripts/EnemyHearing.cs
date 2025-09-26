using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHearing : MonoBehaviour
{
    public static List<EnemyHearing> allEnemies = new List<EnemyHearing>();
    public float hearingSensitivity = 5f;

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
            if (distance <= radius && distance <= enemy.hearingSensitivity)
            {
                enemy.OnHearFootstep(footstepPosition);
            }
        }
    }

    public void OnHearFootstep(Vector3 footstepPosition)
    {
        // Implement enemy reaction to hearing a footstep
        Debug.Log($"{gameObject.name} heard a footstep at {footstepPosition}");
        // For example, move towards the footstep position
        // This is just a placeholder; actual movement logic would depend on your enemy AI implementation
        transform.position = Vector3.MoveTowards(transform.position, footstepPosition, 1f);
    }
}
