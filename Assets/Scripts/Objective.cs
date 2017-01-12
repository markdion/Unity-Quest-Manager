using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Objective : MonoBehaviour
{
    [HideInInspector]
    public int index;
    public string description;
    public ObjectiveType kind;
    [HideInInspector]
    public ObjectiveState state;
    [HideInInspector]
    public Objective nextObjective;
    public ObjectiveTarget[] targets;

    private float progress = 0f;

    public delegate void ObjectiveDelegate(Objective sender);
    public event ObjectiveDelegate OnCompleted;
    public event ObjectiveDelegate OnStarted;

    public Quest ParentScript { get; set; }

    public enum ObjectiveState
    {
        inactive,
        active,
        complete
    }

    public enum ObjectiveType
    {
        travel,
        destroy,
        talk,
        collect
    }

    void Update()
    {
        // Only allow checking for current objective. If a later objective is completed before it is available to the player, 
        // it will be set to complete as soon as it is available since all of its targets are completed
        if (state == ObjectiveState.active)
        {
            if (targets != null && targets.Length > 0)
            {
                switch (kind)
                {
                    case ObjectiveType.destroy:
                        progress = 0f;
                        foreach (var target in targets)
                        {
                            if (target == null || target.gameObject == null)
                            {
                                progress += 1f / targets.Length;
                            }
                        }
                        break;
                    case ObjectiveType.travel:
                        progress = 0f;

                        foreach (var target in targets)
                        {
                            if (target.state == ObjectiveState.complete)
                            {
                                progress += 1f / targets.Length;
                            }
                        }
                        break;
                    case ObjectiveType.talk:
                        break;
                    case ObjectiveType.collect:
                        break;
                }
            }

            if (Mathf.Approximately(1f, progress) && state != ObjectiveState.complete)
            {
                state = ObjectiveState.complete;
                OnCompletedObjective();
            }
        }
    }

    public void Initialize()
    {
        InvokeOnStartedEvent();

        // Spawn anything set for delayed spawn

    }

    public void InvokeOnStartedEvent()
    {
        if(OnStarted != null)
        {
            OnStarted(this);
        }
    }

    public void AssignTargets(ObjectiveTarget[] targets)
    {
        this.targets = targets;
        foreach(var target in this.targets)
        {
            target.kind = kind;
            target.objective = this;
        }
    }

    public void AssignTarget(ObjectiveTarget target)
    {
        target.kind = kind;
        target.objective = this;
        targets = new ObjectiveTarget[] { target };
    }

    private void OnCompletedObjective()
    {
        if (OnCompleted != null)
        {
            OnCompleted(this);
        }
        if (nextObjective != null)
        {
            ParentScript.currentObjective = nextObjective;
            ParentScript.currentObjective.state = ObjectiveState.active;
            ParentScript.currentObjective.Initialize();
        }
        else
        {
            // All objectives complete, end quest
            ParentScript.OnObjectivesCompleted();
        }
        print(string.Format("completed objective: {0}", description));
    }
}
