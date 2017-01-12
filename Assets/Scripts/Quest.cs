using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Quest : MonoBehaviour
{
    public string questName;
    [Multiline]
    public string description;
    [HideInInspector]
    public Objective currentObjective;
    [HideInInspector]
    public Text currentObjectiveDescription;
    public Objective.ObjectiveState state;
    [HideInInspector]
    public Objective[] objectives;

    public delegate void QuestCompletedDelegate(Quest sender);
    public event QuestCompletedDelegate OnCompleted;

    void Start ()
    {
        // Set the quest and the first objective to active
        currentObjective.state = Objective.ObjectiveState.active;
        //currentObjective.InvokeOnStartedEvent();
        GameObject objectiveParentGameObject = currentObjective.transform.parent.gameObject;
        if(objectiveParentGameObject != null)
        {
            objectives = objectiveParentGameObject.GetComponentsInChildren<Objective>();
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
        }
	}

    public Objective GetObjectiveAtIndex(int index)
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
        state = Objective.ObjectiveState.complete;
        if(OnCompleted != null)
        {
            OnCompleted(this);
        }
    }

    public void AddObjective()
    {
        var go = new GameObject();
        var obj = go.AddComponent<Objective>();
        obj.ParentScript = this;
        obj.transform.SetParent(transform);
        obj.gameObject.name = "New Objective";
    }
}
