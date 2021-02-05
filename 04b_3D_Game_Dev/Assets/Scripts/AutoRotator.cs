using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotator : MonoBehaviour {
    [SerializeField] float rotationSpeed;

    private float rotationAngle = 0.0f;

    void Update() {
        rotationAngle += rotationSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);
    }
}

