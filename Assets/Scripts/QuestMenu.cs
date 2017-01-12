using UnityEngine;
using System.Collections;

public class QuestMenu : MonoBehaviour {

    public GameObject questMenuCanvas;

    void Update()
    {
        if (GameManager.instance.isMenuOpen && GameManager.instance.pauseType == GameManager.PauseType.questMenu)
        {
            questMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (!GameManager.instance.isMenuOpen)
        {
            questMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            // If game is already paused because of another menu, don't do anything
            if (GameManager.instance.isMenuOpen && GameManager.instance.pauseType != GameManager.PauseType.questMenu)
            {
                return;
            }
            if (GameManager.instance.isMenuOpen)
            {
                GameManager.instance.pauseType = GameManager.PauseType.none;
            }
            else
            {
                GameManager.instance.pauseType = GameManager.PauseType.questMenu;
            }
            GameManager.instance.isMenuOpen = !GameManager.instance.isMenuOpen;
        }
    }
}
