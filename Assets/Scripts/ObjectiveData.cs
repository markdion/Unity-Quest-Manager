using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Quests
{
    [Serializable]
    public class ObjectiveData
    {
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

        private float progress;

        public int index;
        public string description;
        public ObjectiveType kind;
        public ObjectiveState state;
        public ObjectiveData nextObjective;
        public List<ObjectiveTarget> targets;

        public delegate void ObjectiveDelegate(ObjectiveData sender);
        public event ObjectiveDelegate OnCompleted;
        public event ObjectiveDelegate OnStarted;

        public Quest ParentScript { get; set; }

        public void AssignTargets(IList<ObjectiveTarget> targets)
        {
            this.targets = targets.ToList();
            foreach (var target in this.targets)
            {
                target.kind = kind;
                target.objective = this;
            }
        }

        public void AssignTarget(ObjectiveTarget target)
        {
            target.kind = kind;
            target.objective = this;
            targets = new List<ObjectiveTarget>() { target };
        }

        public void InvokeOnStartedEvent()
        {
            if (OnStarted != null)
            {
                OnStarted(this);
            }
        }
    }
}
