#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SettingsGame))]
public class StaticVariableControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SettingsGame controller = (SettingsGame)target;

        SettingsGame.LoadingSceneWithImage = EditorGUILayout.Toggle(
            "Loading Scene With Image",
            SettingsGame.LoadingSceneWithImage
        );

        if (GUI.changed)
        {
            EditorUtility.SetDirty(controller);
        }
    }
}
#endif
