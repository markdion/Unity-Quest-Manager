using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.Quests;

public class ObjectiveDisplay : MonoBehaviour {

    public Text textName;
    public Image statusImage;
    public Sprite completedSprite;
    public Sprite inProgressSprite;

    [HideInInspector]
    public ObjectiveData objective;

    void Start ()
    {
        if (objective != null)
        {
            Initialize(objective);
        }
    }

    public void Initialize(ObjectiveData objective)
    {
        this.objective = objective;
        if (textName != null)
        {
            textName.text = objective.description;
        }
        if (statusImage != null)
        {
            switch (objective.state)
            {
                case ObjectiveData.ObjectiveState.inactive:
                    gameObject.SetActive(false);
                    break;
                case ObjectiveData.ObjectiveState.active:
                    statusImage.sprite = inProgressSprite;
                    Color currentColor = new Color();
                    ColorUtility.TryParseHtmlString("#7E0092FF", out currentColor);
                    statusImage.color = currentColor;
                    break;
                case ObjectiveData.ObjectiveState.complete:
                    statusImage.sprite = completedSprite;
                    break;
            }
            if(objective.state == ObjectiveData.ObjectiveState.complete)
            {
                statusImage.sprite = completedSprite;
            }
            else if (objective.state == ObjectiveData.ObjectiveState.active)
            {
                statusImage.sprite = inProgressSprite;
            }
        }
    }
}
