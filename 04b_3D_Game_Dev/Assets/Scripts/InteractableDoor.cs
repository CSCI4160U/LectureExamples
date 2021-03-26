using System.Collections;
using UnityEngine;

public class InteractableDoor : InteractableObject {
    [SerializeField] private bool autoOpen = false;

    [SerializeField] private bool autoClose = false;
    [SerializeField] private float autoCloseDelay = 8f;

    [SerializeField] private bool isUnlocked = true;

    [SerializeField] private string openText = "Right-click to open";
    [SerializeField] private string closeText = "Right-click to close";
    [SerializeField] private string lockedText = "Find a key to unlock this door";

    [SerializeField] private Animator animator;

    private AudioSource audioSource;
    
    [SerializeField] private AudioClip soundEffect;
    [SerializeField] private float audioDelay = 0.2f;

    private bool isOpen = false;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if (autoOpen && !isOpen) {
            OpenDoor();
        }
    }

    public override void Activate() {
        if (!isOpen && isUnlocked) {
            OpenDoor();
        } else if (isOpen) {
            CloseDoor();
        }
    }

    private void OpenDoor() {
        isOpen = true;
        animator.SetBool("Open", true);
        audioSource.clip = soundEffect;
        audioSource.PlayDelayed((ulong)(audioDelay * 1000));

        activateText = closeText;

        if (autoClose) {
            StartCoroutine(CloseAfterDelay(autoCloseDelay));
        }
    }

    public IEnumerator CloseAfterDelay(float delay) {
        float timePassed = 0f;

        // wait for 'delay' seconds
        do {
            timePassed += Time.deltaTime;
            yield return null;
        } while (timePassed < delay);

        CloseDoor();

        yield return null;
    }

    private void CloseDoor() {
        isOpen = false;
        animator.SetBool("Open", false);
        activateText = openText;
    }

    public override string GetInteractionText() {
        if (isUnlocked) {
            return activateText;
        } else {
            return lockedText;
        }
    }
}
