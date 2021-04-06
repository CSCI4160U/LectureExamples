using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] public int maxHP = 100;
    [SerializeField] public int hp = 30;
    [SerializeField] private bool isDead = false;
    
    public void TakeDamage(int damageAmount) {
        this.hp -= damageAmount;

        if (this.hp <= 0) {
            isDead = true;
            this.hp = 0;
        } else {
            isDead = false;
        }
    }

    public bool IsDead() {
        return isDead;
    }
}
