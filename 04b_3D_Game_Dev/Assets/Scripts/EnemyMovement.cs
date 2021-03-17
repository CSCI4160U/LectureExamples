using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour {
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float closeEnoughDistance = 1f;
    [SerializeField] private bool repeat = true;

    private NavMeshAgent agent = null;
    private Animator animator = null;
    private int currentWaypointIndex = 0;
    private bool patrolling = true;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        if ((agent != null) && (waypoints.Length > 0)) {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    void Update() {
        if (!patrolling) {
            return;
        }

        float distanceToTarget = Vector3.Distance(agent.transform.position,
                                                  waypoints[currentWaypointIndex].position);
        if (distanceToTarget < closeEnoughDistance) {
            // move to the next waypoint
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length) {
                if (repeat) {
                    currentWaypointIndex = 0;
                } else {
                    patrolling = false;
                    animator.SetFloat("Speed", 0f);
                    return;
                }
            }

            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
