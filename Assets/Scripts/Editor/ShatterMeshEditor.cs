using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShatterMesh))]
public class ShatterMeshEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var targetScript = (ShatterMesh)target;

        if (GUILayout.Button("Setup Shatter"))
        {
            targetScript.SetShatterReady();
        }


        if (GUILayout.Button("Setup Shatter"))
        {
            targetScript.ReleaseKinematics(targetScript.transform.position);
        }
    }

}

