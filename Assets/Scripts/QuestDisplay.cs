using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestDisplay : MonoBehaviour {

    public Text textName;
    public Image sprite;
    public Sprite activeMarker;

    public delegate void ListItemDisplayDelegate(Quest sender);
    public static event ListItemDisplayDelegate OnClick;
    public static event ListItemDisplayDelegate OnPointerEnter;

    public Quest quest;

	void Start ()
    {
	    if(quest != null)
        {
            Initialize(quest);
        }
	}

    public void Initialize(Quest quest)
    {
        Debug.Log(string.Format("Initializing QuestDisplay ({0})", quest.questName));
        this.quest = quest;
        if(textName != null)
        {
            textName.text = quest.questName;
        }
        if(quest.state == Objective.ObjectiveState.active)
        {
            Debug.Log(string.Format("Set quest as active ({0})", quest.questName));
            sprite.color = new Color(255f, 255f, 255f, 255f);
            sprite.sprite = activeMarker;
            Color activeColor = new Color();
            ColorUtility.TryParseHtmlString("#7E0092FF", out activeColor);
            sprite.color = activeColor;
        }
        else if(quest.state == Objective.ObjectiveState.inactive)
        {
            Debug.Log(string.Format("Set quest as inactive ({0})", quest.questName));
            sprite.color = new Color(255f, 255f, 255f, 0f);
            sprite.sprite = null;
        }
    }

    public void Click()
    {
        string displayName = "nothing";
        if(quest != null)
        {
            GameManager.questManager.SetActiveQuest(quest);
        }
        if(OnClick != null)
        {
            Debug.Log(string.Format("Clicked on {0}", displayName));
            OnClick(quest);
        }
        else
        {
            Debug.Log(string.Format("Nobody is listening to {0} Click event", displayName));
        }
    }

    public void PointerEnter()
    {
        string displayName = "nothing";
        if (quest != null)
        {
            displayName = quest.questName;
        }
        if (OnPointerEnter != null)
        {
            Debug.Log(string.Format("Hovered over {0}", displayName));
            OnPointerEnter(quest);
        }
        else
        {
            Debug.Log(string.Format("Nobody is listening to {0} Hover event", displayName));
        }
    }
}
