using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Cannon))]
public class CannonEditor : Editor {
    [DrawGizmo(GizmoType.Pickable | GizmoType.Selected)]
    static void DrawGizmosSelected(Cannon cannon, GizmoType gizmoType) {
        // draw starting point
        var offsetPosition = cannon.transform.TransformPoint(cannon.launchOffset.position);
        Handles.DrawDottedLine(cannon.transform.position, offsetPosition, 3);
        Handles.Label(offsetPosition, "Offset");

        // estimate some points along the trajectory
        var velocity = cannon.transform.forward * cannon.launchSpeed;
        var position = offsetPosition;
        var positions = new List<Vector3>();
        var physicsStep = 0.1f;
        for (var i = 0f; i < 1f; i += physicsStep) {
            positions.Add(position);

            position += velocity * physicsStep;
            velocity += Physics.gravity * physicsStep;
        }

        using (new Handles.DrawingScope(Color.yellow)) {
            // draw a line showing the trajectory
            Handles.DrawAAPolyLine(positions.ToArray());

            // draw a label for this gizmo
            Handles.Label(positions[positions.Count - 1], "Estimated Position (1s)");
        }
    }

    private void OnSceneGUI() {
        var cannon = target as Cannon;
        
        using (var changeCheck = new EditorGUI.ChangeCheckScope()) {
            // determine the desired position
            var newOffset = cannon.transform.InverseTransformPoint(
                Handles.PositionHandle(cannon.transform.TransformPoint(cannon.launchOffset.position), cannon.transform.rotation)
            );

            // record the offset position change for undo purposes
            if (changeCheck.changed) {
                Undo.RecordObject(cannon, "Offset Change");
                cannon.launchOffset.position = newOffset;
            }
        }

        Handles.BeginGUI();
        var rectMin = Camera.current.WorldToScreenPoint(cannon.launchOffset.transform.TransformPoint(Vector3.zero));
        var rect = new Rect();
        rect.xMin = rectMin.x;
        rect.yMin = SceneView.currentDrawingSceneView.position.height - rectMin.y;
        rect.width = 64;
        rect.height = 18;
        GUILayout.BeginArea(rect);

        using (new EditorGUI.DisabledGroupScope(!Application.isPlaying)) {
            if (GUILayout.Button("Fire")) {
                cannon.Fire();
            }
        }


        GUILayout.EndArea();
        Handles.EndGUI();
    }
}
