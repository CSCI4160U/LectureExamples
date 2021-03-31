using UnityEngine;
using UnityEngine.AI;

public class EnemyHealthRagdoll : MonoBehaviour {
    [SerializeField] private int health = 30;
    [SerializeField] private bool isDead = false;
    [SerializeField] private Ragdoller ragdoll;

    private Animator animator = null;
    private NavMeshAgent agent = null;

    void Awake() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(int damage) {
        health -= damage;

        if (animator) {
            animator.SetTrigger("TakeHit");
        }

        if (health <= 0) {
            // we died
            health = 0;
            isDead = true;

            if (agent) {
                agent.enabled = false;
            }

            if (ragdoll) {
                ragdoll.Ragdoll(transform);
                ragdoll.gameObject.SetActive(true);
                transform.gameObject.SetActive(false);
            }

            if (animator) {
                animator.SetBool("IsDead", true);
            }
        }
    }

    public void TakeExplosionDamage(int damage, Vector3 explosionPoint, float forceAmount) {
        health -= damage;

        if (health <= 0) {
            // we died
            health = 0;
            isDead = true;
        }

        if (ragdoll) {
            ragdoll.Ragdoll(transform);
            transform.gameObject.SetActive(false);
            ragdoll.gameObject.SetActive(true);

            Vector3 towardUs = (transform.position - explosionPoint).normalized;
            ragdoll.ApplyExplosiveForce(towardUs, forceAmount);
        }
    }
}
