using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SelectEverySecondChild : EditorWindow
{
    [MenuItem("Tools/Select Every Second Child")]
    static void SelectChildren()
    {
        if (Selection.activeTransform == null)
        {
            Debug.LogWarning("No GameObject selected.");
            return;
        }

        Transform parent = Selection.activeTransform;
        List<GameObject> selectedChildren = new List<GameObject>();

        for (int i = 1; i < parent.childCount; i += 2)
        {
            selectedChildren.Add(parent.GetChild(i).gameObject);
        }

        Selection.objects = selectedChildren.ToArray();
        Debug.Log($"Selected {selectedChildren.Count} child(ren).");
    }
}
