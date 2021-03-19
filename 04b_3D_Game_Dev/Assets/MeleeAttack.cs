using UnityEngine;
using UnityEngine.AI;

public class MeleeAttack : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private float alertDistance = 10f;
    [SerializeField] private float attackDistance = 1f;

    private Animator animator;
    private NavMeshAgent agent;

    void Awake() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        if (animator.GetBool("IsDead")) {
            return;
        }

        Vector3 targetDirection = target.position - transform.position;
        targetDirection.y = 0.0f;

        if (targetDirection.magnitude < attackDistance) {
            // we are close enough to attack
            agent.enabled = false; // stop moving
            animator.SetFloat("Speed", 0f);
            animator.SetInteger("AttackNum", Random.Range(1, 6));
            animator.SetTrigger("Attack");

            // force turn toward the target
            Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.1f);

        } else if (targetDirection.magnitude < alertDistance) {
            // we are close enough to detect the target, start moving toward it

            agent.enabled = true; // start moving
            agent.SetDestination(target.position);
            animator.SetFloat("Speed", agent.velocity.magnitude);
        } else {
            agent.enabled = false; // stop moving
            animator.SetFloat("Speed", 0f);
        }
    }
}
