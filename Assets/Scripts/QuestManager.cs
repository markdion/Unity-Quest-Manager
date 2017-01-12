using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class QuestManager : MonoBehaviour {

    public QuestListDisplay questsDisplayPrefab;
    public Transform questMenuParent;

    public delegate void QuestManagerDelegate(QuestManager sender);
    public delegate void QuestAddedDelegate(Quest addedQuest);
    public event QuestManagerDelegate OnChanged;
    public event QuestAddedDelegate OnQuestAdd;

    private Quest _activeQuest;
    private List<Quest> _quests;

    public List<Quest> Quests
    {
        get
        {
            if(_quests == null)
            {
                _quests = new List<Quest>();
            }
            return _quests;
        }
    }

    public string ActiveObjective
    {
        get
        {
            if(_activeQuest == null || _activeQuest.currentObjective == null)
            {
                return null;
            }
            return _activeQuest.currentObjective.description;
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    private void Quest_OnCompleted(Quest sender)
    {
        Remove(sender);
        if(_activeQuest == sender)
        {
            SetActiveQuest(null);
        }
        if(Quests.Count <= 0)
        {
            //GameManager.instance.DisplayWinScreen();
        }
    }

    public void Display()
    {
        QuestListDisplay display = Instantiate(questsDisplayPrefab) as QuestListDisplay;
        display.transform.SetParent(questMenuParent, false);
        display.Initialize(this);
    }

    public void Add(Quest newQuest)
    {
        if(newQuest == null)
        {
            return;
        }
        _quests.Add(newQuest);
        newQuest.OnCompleted += Quest_OnCompleted;
        if(OnChanged != null)
        {
            OnChanged(this);
        }
        if(OnQuestAdd != null)
        {
            OnQuestAdd(newQuest);
        }
    }

    public void Remove(Quest quest)
    {
        if (quest == null)
        {
            return;
        }
        if (!_quests.Contains(quest))
        {
            return;
        }
        quest.OnCompleted -= Quest_OnCompleted;
        _quests.Remove(quest);
        if (OnChanged != null)
        {
            OnChanged(this);
        }
    }

    /// <summary>
    /// Sets the provided quest to active and all others to inactive
    /// </summary>
    /// <param name="selectedQuest">null to set all quests to inactive</param>
    public void SetActiveQuest(Quest selectedQuest)
    {
        if(selectedQuest == null || _quests.Count <= 0)
        {
            _activeQuest = null;
        }
        else
        {
            foreach (Quest quest in _quests)
            {
                if (quest == selectedQuest)
                {
                    quest.state = Objective.ObjectiveState.active;
                    _activeQuest = quest;
                }
                else
                {
                    quest.state = Objective.ObjectiveState.inactive;
                }
            }
        }
    }

    public Vector3[] GetActiveQuestObjectiveTargets()
    {
        List<Vector3> targets = new List<Vector3>();

        if(_activeQuest != null)
        {
            foreach (ObjectiveTarget target in _activeQuest.currentObjective.targets)
            {
                if(target != null)
                {
                    targets.Add(target.transform.position);
                }
            }
        }
        else
        {
            return null;
        }

        return targets.ToArray();
    }
}
