using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float jumpForce = 400f;

    [SerializeField] private Transform footPosition;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float closeEnough = 0.1f;

    private Rigidbody2D rb;
    private Animator controller;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<Animator>();
    }

    private void Update() {
        float movementX = Input.GetAxis("Horizontal") * movementSpeed;
        Debug.Log(movementX);

        RaycastHit2D hit = Physics2D.Raycast(footPosition.position, Vector2.down, closeEnough, groundLayers);

        if (hit.collider) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb.AddForce(Vector3.up * jumpForce);
            controller.SetTrigger("IsJumping");
        }

        Vector3 movement = Vector3.right * movementX * Time.deltaTime;
        transform.Translate(movement);
        controller.SetFloat("Speed", Mathf.Abs(movementX));
        if (movementX < 0f) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
