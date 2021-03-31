using UnityEngine;

public class InteractableDrawer : InteractableObject {
    [SerializeField] private string animatorParam;
    [SerializeField] private Animator animator;

    private bool isOpen = false;

    public override void Activate() {
        ToggleDrawer();
    }

    private void ToggleDrawer() {
        isOpen = !isOpen;
        if (animator) {
            animator.SetBool(animatorParam, isOpen);
        }
    }
}
