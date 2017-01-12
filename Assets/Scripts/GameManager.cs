using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Quests;

public class GameManager : MonoBehaviour
{
    public Quest firstQuest;

    [HideInInspector]
    public bool isMenuOpen = false;
    [HideInInspector]
    public PauseType pauseType = PauseType.none;

    public static GameManager instance;
    public static QuestManager questManager;

    public enum PauseType
    {
        none,
        pauseMenu,
        questMenu,
        gameOver
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);

        questManager = gameObject.GetComponent<QuestManager>();
    }

	void Start ()
    {
        Invoke("InitializeTargetPracticeQuest", 1f);
    }

    /// <summary>
    /// Initialize the 'Target Practice' quest and add it to the Quest Manager
    /// </summary>
    private void InitializeTargetPracticeQuest()
    {
        //// OBJECTIVE 1 - TRAVEL TO DEIMOS
        //ObjectiveTarget deimos = deimosTravelObjective.AddComponent<ObjectiveTarget>();
        //ObjectiveData firstObjective = firstQuest.GetObjectiveAtIndex(0);
        //firstObjective.AssignTarget(deimos);

        //// OBJECTIVE 2 - DEFEAT RAIDERS
        //SpawnPrefabsForObjective(firstQuest, 1, enemyShipPrefab, 5, deimosSpawnPoint.position, 50);

        //// OBJECTIVE 3 - TRAVEL TO EARTH
        //ObjectiveTarget earth = earthTravelObjective.AddComponent<ObjectiveTarget>();
        //ObjectiveData thirdObjective = firstQuest.GetObjectiveAtIndex(2);
        //thirdObjective.AssignTarget(earth);
        //// Setup spawn of enemies and friendlies so they only spawn when player reaches this objective
        //thirdObjective.OnStarted += ThirdObjective_OnStarted;
        ////TODO: unsubscribe to this!!!

        // OBJECTIVE 4 - AID IN THE FIGHT
        // AI only start battling when player approaches the area

        questManager.Add(firstQuest);
        questManager.SetActiveQuest(firstQuest);
    }

    private void ThirdObjective_OnStarted(ObjectiveData sender)
    {
        
    }
}
