using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
    /*
    private int age = 22;
    [SerializeField] private string firstName = "Randy";
    */

    [SerializeField] private Rigidbody projectilePrefab;
    [SerializeField] private Transform launchOffset;
    [SerializeField] private float launchSpeed = 10f;


    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Fire();
        }
    }

    [ContextMenu("Fire")]
    void Fire() {
        var newProjectile = Instantiate(projectilePrefab);
        newProjectile.position = launchOffset.position;
        newProjectile.velocity = launchSpeed * transform.forward;

        Destroy(newProjectile, 3f);
    }
}
