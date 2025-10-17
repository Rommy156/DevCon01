using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event System.Action OnPlayerSpotted;
    public Transform pathHolder;
    public float speed = 3.5f;
    public float waitTime = 0.5f;
    public float turnSpeed = 90f;
    public float timeToSpotPlayer = 2f;
    public Light spotlight;
    public float viewDistance = 10f;
    public LayerMask viewMask;

   
    private float playerVisibleTimer;

    Animator animator;
    Transform player;
    float viewAngle;
    public bool heardFootstep;
    public Vector3 lastHeardPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotlight.spotAngle;

        // collect waypoints
        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            Vector3 pos = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(pos.x, transform.position.y, pos.z);
        }

        StartCoroutine(FollowPath(waypoints));
    }

    void Update()
    {
       
        // check vision each frame
        if (CanSeePlayer())
        {
            playerVisibleTimer += Time.deltaTime;
            spotlight.color = Color.red;
        }
        else
        {
            playerVisibleTimer -= Time.deltaTime;
            spotlight.color = Color.white;
        }
        ///clamp timer
        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
        spotlight.color = Color.Lerp(spotlight.color, Color.red, playerVisibleTimer / timeToSpotPlayer);
        
        if (playerVisibleTimer >= timeToSpotPlayer)
        {
            if (OnPlayerSpotted != null)
            {
                OnPlayerSpotted();
            }
        }
        if (heardFootstep)
        {
            transform.position = Vector3.MoveTowards(transform.position, lastHeardPosition, speed * Time.deltaTime);
        }

    }

    bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);

            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                {
                    Debug.DrawLine(transform.position, player.position, Color.red);
                    return true;
                }
            }
        }
        return false;

    }

    IEnumerator FollowPath(Vector3[] wayPoints)
    {
        transform.position = wayPoints[0];
        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = wayPoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);

        while (true)
        {
            // move to target
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            if (transform.position == targetWaypoint)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % wayPoints.Length;
                targetWaypoint = wayPoints[targetWaypointIndex];

                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(TurnToFace(targetWaypoint));
            }
            yield return null;
        }
    }

    IEnumerator TurnToFace(Vector3 lookTarget)
    {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = Mathf.Atan2(dirToLookTarget.x, dirToLookTarget.z) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    void OnDrawGizmos()
    {
        if (pathHolder != null)
        {
            Vector3 startPos = pathHolder.GetChild(0).position;
            Vector3 previousPos = startPos;

            foreach (Transform waypoint in pathHolder)
            {
                Gizmos.DrawSphere(waypoint.position, 0.3f);
                Gizmos.DrawLine(previousPos, waypoint.position);
                previousPos = waypoint.position;
            }
            Gizmos.DrawLine(previousPos, startPos);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
    public CaughtUIScript caughtMeter;
    void OnplayerDetected() { caughtMeter.playerVisible = true; }
    void OnplayerLost() { caughtMeter.playerVisible = false; }
}

