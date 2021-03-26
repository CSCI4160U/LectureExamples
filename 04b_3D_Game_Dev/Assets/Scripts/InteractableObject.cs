using UnityEngine;

public class InteractableObject : MonoBehaviour {
    [SerializeField] protected string activateText = "Right-click to interact.";

    public virtual void Activate() {
        Debug.Log(gameObject.name + "::Activate()");
    }

    public virtual string GetInteractionText() {
        return activateText;
    }
}
