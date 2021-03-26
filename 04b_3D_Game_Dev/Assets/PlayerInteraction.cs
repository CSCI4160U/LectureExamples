using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour {
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float range = 2f;
    [SerializeField] private Text interactionText;
    [SerializeField] private LayerMask interactableLayers;

    private void Update() {
        RaycastHit hit;
        InteractableObject interactable = null;
        if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, range, interactableLayers)) {
            // get the interactable object
            interactable = hit.collider.GetComponent<InteractableObject>();
            Debug.Log(interactable);
            if (interactable) {
                interactionText.text = interactable.GetInteractionText();
            } else {
                interactionText.text = "";
            }
        } else {
            interactionText.text = "";
        }

        if (Input.GetButtonDown("Fire2") && interactable) {
            interactable.Activate();
        }
    }
}
