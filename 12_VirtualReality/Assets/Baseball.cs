using System.Collections.Generic;
using UnityEngine;

public class Baseball : MonoBehaviour {
    [SerializeField] private float groundThreshold = 0.2f;

    [SerializeField] private bool showTrail = true;
    [SerializeField] private GameObject ballImagePrefab = null;
    [SerializeField] private float ballImageInterval = 0.1f;

    private Vector3 originalPosition;
    private Vector3 hitPosition = Vector3.zero;
    private Vector3 groundHitPosition;
    private bool isGrounded = false;

    private float lastImageTime;

    private void Start() {
        originalPosition = transform.position; // spawn position

        lastImageTime = Time.time;
    }

    private void Update() {
        // show the trail
        if (!isGrounded && showTrail && (Time.time > (lastImageTime + ballImageInterval))) {
            var ballImage = Instantiate(ballImagePrefab, transform.position, transform.rotation);
            lastImageTime = Time.time;
        }

        // monitor for ground hit
        if (!isGrounded && transform.position.y <= groundThreshold) {
            isGrounded = true;
            groundHitPosition = transform.position;

            Debug.Log("The ball travelled " + Vector3.Distance(hitPosition, groundHitPosition) + ".m");
        }
    }
}
