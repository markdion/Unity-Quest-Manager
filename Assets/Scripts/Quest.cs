using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Assets.Scripts.Quests;

public class Quest : MonoBehaviour
{
    public string questName;
    [Multiline]
    public string description;
    public ObjectiveData currentObjective;
    public Text currentObjectiveDescription;
    public ObjectiveData.ObjectiveState state;
    public List<ObjectiveData> objectives = new List<ObjectiveData>();

    public delegate void QuestCompletedDelegate(Quest sender);
    public event QuestCompletedDelegate OnCompleted;

    void Start ()
    {
        // Set the quest and the first objective to active
        currentObjective.state = ObjectiveData.ObjectiveState.active;
        //currentObjective.InvokeOnStartedEvent();
        //GameObject objectiveParentGameObject = currentObjective.transform.parent.gameObject;
        //if(objectiveParentGameObject != null)
        //{
        //    objectives = new List<ObjectiveData>(objectiveParentGameObject.GetComponentsInChildren<ObjectiveData>());
            if(objectives != null)
            {
                Debug.Log("Successfully found all mission objectives");
                foreach(var obj in objectives)
                {
                    if(obj != null)
                    {
                        obj.ParentScript = this;
                    }
                }
            }
            else
            {
                Debug.Log("Failed to find mission objectives");
            }
        //}
	}

    public void AddObjective()
    {
        if (objectives == null)
        {
            objectives = new List<ObjectiveData>();
        }

        var obj = new ObjectiveData();

        obj.index = objectives.Count;
        objectives.Add(obj);

        if (obj != null)
        {
            obj.ParentScript = this;
        }
    }

    public ObjectiveData GetObjectiveAtIndex(int index)
    {
        foreach(var obj in objectives)
        {
            if(obj.index == index)
            {
                return obj;
            }
        }
        return null;
    }

    public void OnObjectivesCompleted()
    {
        print(string.Format("completed quest: {0}", questName));
        state = ObjectiveData.ObjectiveState.complete;
        if(OnCompleted != null)
        {
            OnCompleted(this);
        }
    }
}
