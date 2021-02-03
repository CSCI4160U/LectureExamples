using UnityEngine;
using UnityEngine.UI;

public class EnemyCollisionDetector : MonoBehaviour {
    [SerializeField] private Slider healthSlider;

    private Health health;

    private void Awake() {
        health = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            healthSlider.value -= 10f;
            health.health -= 10f;
        }
    }
}
