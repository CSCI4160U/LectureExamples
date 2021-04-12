using System.Collections;
using UnityEngine;

public class AutoPitcher : MonoBehaviour {
    [SerializeField] private Transform attachPoint = null;
    [SerializeField] private GameObject baseballPrefab = null;

    [SerializeField] private Transform pitchingTarget = null;
    [SerializeField] private float throwingForce = 200f;

    [SerializeField] private float throwDelay = 5f;

    private Animator animator;
    private GameObject baseball;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        animator.SetTrigger("Throw");
    }

    public void PickUpBall() {
        baseball = Instantiate(baseballPrefab, attachPoint);
        baseball.transform.position = attachPoint.transform.position;
        Rigidbody baseballRB = baseball.GetComponent<Rigidbody>();
        baseballRB.isKinematic = true;
    }

    public void Release() {
        // restore the object as a rigid body
        baseball.transform.SetParent(null);
        Rigidbody baseballRB = baseball.GetComponent<Rigidbody>();
        baseballRB.isKinematic = false;

        // throwing force
        Vector3 direction = (pitchingTarget.position - baseball.transform.position).normalized;
        baseballRB.AddForce(direction * throwingForce, ForceMode.Force);

        // get ready for the next throw, after a delay
        StartCoroutine(WaitThenThrow(throwDelay));
    }

    IEnumerator WaitThenThrow(float delay) {
        float timePassed = 0f;
        do {
            timePassed += Time.deltaTime;
            yield return null;
        } while (timePassed < delay);

        animator.SetTrigger("Throw");

        yield return null;
    }
}
