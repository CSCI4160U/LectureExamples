using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState {
    Patrolling,
    Alerted,
    TargetVisible,
    Dead
}

public class EnemyAIStateMachine : MonoBehaviour {
    [SerializeField] private EnemyState currentState = EnemyState.Patrolling;

    [Header("Patrolling")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private int waypointIndex;
    [SerializeField] private bool patrolLoop = true;
    [SerializeField] private float closeEnoughDistance = 1f;

    [Header("Alerted")]
    [SerializeField] private float lastAlertTime = 0f;
    [SerializeField] private float alertCooldown = 8f;
    [SerializeField] private Vector3 lastKnownTargetPosition;

    [Header("TargetVisible")]
    [SerializeField] private float lastShootTime = 0f;
    [SerializeField] private float shootCooldown = 1f;
    [SerializeField] private Transform target;

    private Animator animator;
    private NavMeshAgent agent;

    private void Awake() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start() {
        currentState = EnemyState.Patrolling;

        if ((agent != null) && (waypoints.Length > 0)) {
            agent.SetDestination(waypoints[waypointIndex].position);
        }
    }

    public EnemyState GetState() {
        return currentState;
    }

    public void SetState(EnemyState newState) {
        if (currentState == newState) {
            // only do this stuff when transitioning to a new state
            return;
        }

        currentState = newState;
        if (newState == EnemyState.Patrolling) {
            // go back to our patrol
            agent.enabled = true;
            waypointIndex = 0;
            agent.SetDestination(waypoints[waypointIndex].position);
        } else if (newState == EnemyState.Alerted) {
            // investigate the last known target position
            agent.enabled = true;
            agent.SetDestination(lastKnownTargetPosition);

            // remember when we were alerted
            lastAlertTime = Time.time;
        } else if (newState == EnemyState.TargetVisible) {
            // stop and shoot
            agent.enabled = false;
            animator.SetFloat("Speed", 0f);

            // remember the target's last known position
            lastKnownTargetPosition = target.position;
        } else if (newState == EnemyState.Dead) {
            // disable navigation
            agent.enabled = false;
            animator.SetFloat("Speed", 0f);
            animator.SetBool("Dead", true);
        }
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }

    void Update() {
        if (currentState == EnemyState.Dead) {
            return;
        } else if (currentState == EnemyState.Patrolling) {
            Patrol();
        } else if (currentState == EnemyState.Alerted) {
            if (Time.time > (lastAlertTime + alertCooldown)) {
                SetState(EnemyState.Patrolling);
                Patrol();
            } else {
                Alert();
            }
        } else if (currentState == EnemyState.TargetVisible) {
            Shoot();
        }
    }

    private void Patrol() {
        Vector3 destination = waypoints[waypointIndex].position;
        float distanceToDestination = Vector3.Distance(agent.transform.position, destination);
        if (distanceToDestination < closeEnoughDistance) {
            // we reached the waypoint
            waypointIndex++;

            if (waypointIndex >= waypoints.Length) {
                if (patrolLoop) {
                    // start over
                    waypointIndex = 0;
                } else {
                    // just stop and look cool
                    animator.SetFloat("Speed", 0f);
                    return;
                }
            }

            agent.SetDestination(waypoints[waypointIndex].position);
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void Alert() {
        float distanceToTarget = Vector3.Distance(agent.transform.position, lastKnownTargetPosition);

        if (distanceToTarget < closeEnoughDistance) {
            // stay here
            animator.SetFloat("Speed", 0f);

            // TODO: Play a look around animation
        } else {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
    }

    private void Shoot() {
        if (Time.time > (lastShootTime + shootCooldown)) {
            // turn towards target
            Vector3 targetDirection = (target.transform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);

            // shoot
            lastShootTime = Time.time;
            animator.SetTrigger("Shoot");

            // TODO:  do the raycast and take damage

        }
    }
}
