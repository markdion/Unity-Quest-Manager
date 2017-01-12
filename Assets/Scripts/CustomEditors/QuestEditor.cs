using Assets.Scripts.Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{
    private ReorderableList objectivesList;

    private void OnEnable()
    {
        objectivesList = new ReorderableList(serializedObject,
                serializedObject.FindProperty("objectives"),
                true, true, true, true);
    }

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        var myQuest = (Quest)target;

        myQuest.questName = EditorGUILayout.TextField("Name", myQuest.questName);
        myQuest.description = EditorGUILayout.TextField("Description", myQuest.description, GUILayout.MaxHeight(75));

        serializedObject.Update();
        objectivesList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();

        //if (GUILayout.Button("Add Objective"))
        //{
        //    myQuest.AddObjective();
        //}

        objectivesList.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = objectivesList.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;
                EditorGUI.PropertyField(
                    new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("kind"), GUIContent.none);
                EditorGUI.PropertyField(
                    new Rect(rect.x + 60, rect.y, rect.width - 150, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("description"), GUIContent.none);
                EditorGUI.PropertyField(
                    new Rect(rect.x + rect.width - 70, rect.y, 30, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("targets"), GUIContent.none);
            };

        //Vector2 scrollPosition = new Vector2(0, 0);
        //scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        //for (int i = 0; i < myQuest.objectives.Count; i++)
        //{
        //    var obj = myQuest.objectives[i];
        //    bool showPosition = false;
        //    showPosition = EditorGUILayout.Foldout(showPosition, string.IsNullOrEmpty(obj.description) ? "Objective " + obj.index : obj.description);
        //    if (showPosition)
        //    {
        //        obj = EditorGUILayout.ObjectField(obj, typeof(Objective), false) as Objective;
        //    }
        //}

        //EditorGUILayout.EndScrollView();
    }
}
