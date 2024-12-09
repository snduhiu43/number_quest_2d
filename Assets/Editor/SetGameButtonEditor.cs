using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SetGameButton))]
[CanEditMultipleObjects]
[System.Serializable]

public class SetGameButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SetGameButton myScript = target as SetGameButton;

        switch (myScript.ButtonType)
        {
            case SetGameButton.EButtonType.LevelNumberBtn:
                myScript.LevelNumber = (GameSettings.ELevelNumber)EditorGUILayout.EnumPopup("Levels Numbers", myScript.LevelNumber);
                break;
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }


}
