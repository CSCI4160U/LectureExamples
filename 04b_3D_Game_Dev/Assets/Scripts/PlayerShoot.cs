using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    [SerializeField] private Transform mainCamera = null;
    [SerializeField] float range = 100f;

    [SerializeField] private LayerMask wallLayers;
    [SerializeField] private LayerMask enemyLayers;

    [SerializeField] private Animator gunAnimator;
    [SerializeField] private GameObject bulletHolePrefab;

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    private void Shoot() {
        if (gunAnimator != null) {
            gunAnimator.SetTrigger("Shoot");
        }

        RaycastHit hit;

        Debug.Log("Shoot()");
        if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, range, enemyLayers)) {
            Debug.Log("Hit enemy: " + hit.collider.name);

            Health enemyHealth = hit.collider.GetComponent<Health>();
            if (enemyHealth != null) {
                enemyHealth.TakeDamage(10);
            }
        } else if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, range, wallLayers)) {
            //Debug.Log("Hit wall: " + hit.collider.name);

            Instantiate(
                bulletHolePrefab,
                hit.point + (0.01f * hit.normal),
                Quaternion.LookRotation(-1 * hit.normal, hit.transform.up)
            );
        }

    }
}
