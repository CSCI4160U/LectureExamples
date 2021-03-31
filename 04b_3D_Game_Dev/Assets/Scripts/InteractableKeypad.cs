using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableKeypad : InteractableObject {
    [SerializeField] private float audioDelay = 0.2f;
    [SerializeField] private InteractableDoor doorToUnlock;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public override void Activate() {
        Unlock();
    }

    private void Unlock() {
        if (doorToUnlock) {
            doorToUnlock.Unlock();
        }

        audioSource.PlayDelayed((ulong)(audioDelay * 1000));
    }
}
