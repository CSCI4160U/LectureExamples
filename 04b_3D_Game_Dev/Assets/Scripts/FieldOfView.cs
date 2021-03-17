using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour {
    [SerializeField] private Transform eye;
    [Range(0, 180)] [SerializeField] private float fovAngle = 60f;
    [SerializeField] private float visionRadius = 10f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float targetScanDelay = 0.25f;

    [SerializeField] private bool isAlerted = false;
    [SerializeField] private EnemyAIStateMachine stateMachine = null;

    public void OnDrawGizmos() {
        Vector3 focusPos = eye.position + eye.forward * visionRadius;
        focusPos.y = eye.position.y;

        if (stateMachine != null) {
            if (stateMachine.GetState() == EnemyState.TargetVisible) {
                Gizmos.color = Color.red;
            }
            else if (stateMachine.GetState() == EnemyState.Alerted) {
                Gizmos.color = Color.yellow;
            }
        }
        else {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawWireSphere(eye.position, 0.2f);
        Gizmos.DrawWireSphere(eye.position, visionRadius);
        Gizmos.DrawLine(eye.position, focusPos);

    }

    void Start() {
        StartCoroutine("KeepSearchingForTargets", targetScanDelay);

        stateMachine = GetComponent<EnemyAIStateMachine>();
    }

    IEnumerator KeepSearchingForTargets(float delay) {
        while (true) {
            yield return new WaitForSeconds(delay);
            LookForTargets();
        }
    }

    private void LookForTargets() {
        // see if there is a target in our field of vision
        bool canSeeTarget = false;
        Collider[] targets = Physics.OverlapSphere(eye.position, visionRadius, targetLayer);
        for (int i = 0; i < targets.Length; i++) {
            Vector3 targetPos = targets[i].transform.position;
            targetPos.y += 1f;

            Vector3 targetDirection = (targetPos - eye.position).normalized;

            if (Vector3.Angle(eye.forward, targetDirection) < (fovAngle / 2)) {
                float distance = Vector3.Distance(eye.position, targetPos);
                if (!Physics.Raycast(eye.position, targetDirection, distance, wallLayer)) {
                    canSeeTarget = true;
                    isAlerted = true;

                    // change to target visible state
                    stateMachine.SetTarget(targets[i].transform);
                    stateMachine.SetState(EnemyState.TargetVisible);
                    return;
                }
            }
        }

        if (!canSeeTarget && isAlerted) {
            // change to alerted state
            stateMachine.SetState(EnemyState.Alerted);
            isAlerted = false;
        }
    }

    void Update() {

    }
}
