using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour {
    [SerializeField] private float radius = 3f;
    [SerializeField] private GameObject explosionEffect = null;
    [SerializeField] private Transform explosionPoint = null;
    [SerializeField] private int damage = 100;
    [SerializeField] private bool isDestroyed = false;
    [SerializeField] private float forceAmount = 50000f;

    private List<Barrel> barrelsToExplode = null;

    private void Start() {
        barrelsToExplode = new List<Barrel>();
    }

    public void Explode() {
        if (explosionEffect) {
            Instantiate(explosionEffect, explosionPoint.position, Quaternion.identity);
        }
        Destroy(transform.gameObject, 0.25f);

        LayerMask playerMask = LayerMask.GetMask("GoodGuys");
        Collider[] hits = Physics.OverlapSphere(explosionPoint.position, radius, playerMask);
        for (int i = 0; i < hits.Length; i++) {
            Health health = hits[0].GetComponent<Health>();
            health.TakeDamage(damage);
        }

        LayerMask enemyMask = LayerMask.GetMask("Enemies");
        hits = Physics.OverlapSphere(explosionPoint.position, radius, enemyMask);
        for (int i = 0; i < hits.Length; i++) {
            EnemyHealthRagdoll health = hits[0].GetComponent<EnemyHealthRagdoll>();
            if (health) {
                health.TakeExplosionDamage(damage, explosionPoint.position, forceAmount);
            }
        }
    }
}
