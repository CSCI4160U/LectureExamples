using UnityEngine;

public class Ragdoller : MonoBehaviour {
    [SerializeField] private Rigidbody mainBody = null;

    public void Ragdoll(Transform newTransform) {
        // move the ragdoll child game object into the same spot as the animated child
        transform.position = newTransform.position;
        transform.rotation = newTransform.rotation;
    }

    public void ApplyForceToBodyPart(Rigidbody body, Vector3 forceDirection, float forceAmount) {
        if (body != null && forceAmount > 0) {
            body.AddForce(forceDirection * forceAmount);
        }
    }

    public void ApplyExplosiveForce(Vector3 forceDirection, float forceAmount) {
        if (mainBody != null && forceAmount > 0) {
            mainBody.AddForce(forceDirection * forceAmount);
        }
    }
}
