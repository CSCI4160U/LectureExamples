using UnityEngine;

public class Health : MonoBehaviour {
    //[SerializeField] private int maxHP = 100;
    [SerializeField] private int hp = 30;
    [SerializeField] private bool isDead = false;
    
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damageAmount) {
        this.hp -= damageAmount;

        if (this.hp <= 0) {
            isDead = true;
            this.hp = 0;

            if (animator != null) {
                animator.SetBool("IsDead", true);
                animator.SetBool("Attack", false);
                animator.SetFloat("Speed", 0f);
            }
        } else {
            isDead = false;

            if (animator != null) {
                animator.SetTrigger("TakeHit");
            }
        }
    }

    public bool IsDead() {
        return isDead;
    }
}
