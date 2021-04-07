using UnityEngine;
public class PlayerShoot : MonoBehaviour {
    [SerializeField] private Transform mainCamera = null;
    [SerializeField] float range = 100f;
    [SerializeField] private LayerMask wallLayers;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private LayerMask shieldLayers;
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private GameObject bulletHolePrefab;
    [SerializeField] private float initialHitRadius = 0.5f;
    [SerializeField] private float hitRadiusDecay = 0.01f;

    private Material lastShieldHitMaterial = null;
    private float hitRadius = 0f;
    
    private void Update() { 
        if (lastShieldHitMaterial) {
            hitRadius -= hitRadiusDecay;
            lastShieldHitMaterial.SetFloat("HitRadius", hitRadius);
        }
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    private void Shoot() {
        if (gunAnimator != null) {
            gunAnimator.SetTrigger("Shoot");
        }

        RaycastHit hit;
        if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, range, shieldLayers)) {
            Debug.Log("Hit shield: " + hit.collider.name);
            // update hit position and radius
            EnergyShield shield = hit.collider.GetComponent<EnergyShield>();
            shield.RegisterHit(10, hit.point);
        } else if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, range, enemyLayers)) {
            Debug.Log("Hit enemy: " + hit.collider.name);
            Health enemyHealth = hit.collider.GetComponent<Health>();
            if (enemyHealth != null) {
                enemyHealth.TakeDamage(10);
            }
        } else if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, range, wallLayers)) {
            Debug.Log("Hit wall: " + hit.collider.name);
            Instantiate(
            bulletHolePrefab,
            hit.point + (0.01f * hit.normal),
            Quaternion.LookRotation(-1 * hit.normal, hit.transform.up)
            );
        }
    }
}