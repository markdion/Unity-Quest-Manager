using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var myQuest = (Quest)target;

        var style = new GUIStyle();
        style.wordWrap = true;
        style.margin = new RectOffset(0, 0, 10, 10);

        GUILayout.Label("Organize the quest objectives sequentially as children of this GameObject in the Hierarchy. \n\nAdd a new Objective by pressing the button below.", style);

        if (GUILayout.Button("Add Objective"))
        {
            myQuest.AddObjective();
        }
    }
}
