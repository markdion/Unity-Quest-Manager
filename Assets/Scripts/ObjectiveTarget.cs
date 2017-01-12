using UnityEngine;
using System.Collections;
using Assets.Scripts.Quests;

public class ObjectiveTarget : MonoBehaviour {

    public bool visibleIndicator;
    public ObjectiveData.ObjectiveState state;
    public ObjectiveData.ObjectiveType kind;
    public ObjectiveData objective;

    void Start()
    {
        state = ObjectiveData.ObjectiveState.active;
        visibleIndicator = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (objective == null)
            {
                Debug.LogError("Objective target had no objective set");
                return;
            }

            if (objective.state == ObjectiveData.ObjectiveState.active && (kind == ObjectiveData.ObjectiveType.collect || kind == ObjectiveData.ObjectiveType.travel))
            {
                state = ObjectiveData.ObjectiveState.complete;
            }
        }
    }
}
