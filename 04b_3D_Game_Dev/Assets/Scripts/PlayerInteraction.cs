using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour {
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float range = 2f;

    [SerializeField] private Text interactionText;

    [SerializeField] private LayerMask interactableLayers;
    [SerializeField] private LayerMask collectibleLayers;

    private void Update() {
        RaycastHit hit;

        InteractableObject interactable = null;
        Collectible collectibleItem = null;

        if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, range, collectibleLayers)) {
            // get the collectible object
            collectibleItem = hit.collider.GetComponent<Collectible>();

            if (collectibleItem != null) {
                interactionText.text = collectibleItem.GetInteractionText();
            } else {
                interactionText.text = "";
            }
        } else if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, range, interactableLayers)) {
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

        if (Input.GetButtonDown("Fire2") && collectibleItem) {
            collectibleItem.Activate();
        } else if (Input.GetButtonDown("Fire2") && interactable) {
            interactable.Activate();
        }
    }
}
