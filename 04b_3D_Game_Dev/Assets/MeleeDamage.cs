using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour {
    [SerializeField] private int damage = 5;

    private void OnTriggerEnter(Collider other) {
        Health health = other.GetComponent<Health>();
        if (health != null) {
            health.TakeDamage(damage);
        }
    }
}
