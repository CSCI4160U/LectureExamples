using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float jumpForce = 400f;

    [SerializeField] private Transform footPosition;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float closeEnough = 0.1f;

    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        float movementX = Input.GetAxis("Horizontal") * movementSpeed;

        RaycastHit2D hit = Physics2D.Raycast(footPosition.position, Vector2.down, closeEnough, groundLayers);

        if (hit.collider) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb.AddForce(Vector3.up * jumpForce);
        }

        Vector3 movement = Vector3.right * movementX * Time.deltaTime;
        transform.Translate(movement);
    }
}
