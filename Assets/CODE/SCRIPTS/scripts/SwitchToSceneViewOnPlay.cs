// PURPOSE:
//  This script will automatically switch you to Scene View
//  every time you enter Play Mode, instead of the built-in
//  behavior of switching you to Game View.
//
// USAGE:
//  Add this script file to your project's assets folder and
//  it will load automatically. You can enable or disable the
//  feature via the Tools menu. You can also change the
//  PREF_NAME value below to make the menu setting unique per
//  project instead of global.
//
// AUTHOR: aurycat
// LICENSE: MIT
// HISTORY:
//  1.0 (2022-05-14)
//  1.1 (2022-05-15) Add menu button to turn it off, and also
//       made it not switch to scene view when uploading the
//       avatar or world.

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

[InitializeOnLoadAttribute]
public static class SwitchToSceneViewOnPlay
{
    // Optionally, you can change PREF_NAME to something different
    // for each project so that the setting becomes per-project
    // instead of global. For example, add .<projectname> to the end.
    private const string PREF_NAME = "aurycat.SwitchToSceneViewOnPlay";

    private const string MENU_NAME = "Tools/Switch to Scene View on Play";

    private static bool enabled = true;

    static SwitchToSceneViewOnPlay() {
        EditorApplication.playModeStateChanged += PlayModeStateChanged;
        enabled = EditorPrefs.GetBool(PREF_NAME, true);
        EditorApplication.delayCall += () => {
            Menu.SetChecked(MENU_NAME, enabled);
        };
    }

    [MenuItem(MENU_NAME)]
    private static void ToggleAction() {
        enabled = !enabled;
        EditorPrefs.SetBool(PREF_NAME, enabled);
        Menu.SetChecked(MENU_NAME, enabled);
    }

    private static void PlayModeStateChanged(PlayModeStateChange state) {
        if (enabled && state == PlayModeStateChange.EnteredPlayMode) {
            // Don't switch to scene view if we're uploading an avatar or world
            if (GameObject.Find("/VRCSDK") != null) {
                return;
            }

            Type sceneTab = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.SceneView");
            EditorWindow.GetWindow(sceneTab).Show();
        }
    }
}
#endif