using UnityEngine;
using System.Collections;

public class ObjectiveTarget : MonoBehaviour {

    public bool visibleIndicator;
    public Objective.ObjectiveState state;
    public Objective.ObjectiveType kind;
    public Objective objective;

    void Start()
    {
        state = Objective.ObjectiveState.active;
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

            if (objective.state == Objective.ObjectiveState.active && (kind == Objective.ObjectiveType.collect || kind == Objective.ObjectiveType.travel))
            {
                state = Objective.ObjectiveState.complete;
            }
        }
    }
}
