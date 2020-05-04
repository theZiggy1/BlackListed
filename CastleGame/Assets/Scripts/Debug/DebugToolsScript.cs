using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugToolsScript : MonoBehaviour
{
    // Level names
    [SerializeField]
    private string level1Name;
    [SerializeField]
    private string level2Name;
    [SerializeField]
    private string level3Name;
    [SerializeField]
    private string level4Name;

    [SerializeField]
    private string currentLevelName;
    [SerializeField]
    private GameObject playerInputManager;

    

    // Start is called before the first frame update
    void Start()
    {
        playerInputManager = GameObject.FindGameObjectWithTag("PlayerInputManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Changes to level 1
            ChangeLevels(level1Name);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Changes to level 2
            ChangeLevels(level2Name);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // Changes to level 2
            ChangeLevels(level3Name);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // Changes to level 2
            ChangeLevels(level4Name);
        }
    }

    private void ChangeLevels(string levelName)
    {
        // Sets the current level we're on
        currentLevelName = playerInputManager.GetComponent<PlayerSelectionScript>().sceneNameToLoad;
        // Sets this as the level to unload
        playerInputManager.GetComponent<PlayerSelectionScript>().sceneNameToUnload = currentLevelName;
        // Sets the new level to load to be the one we've hit the number key for
        playerInputManager.GetComponent<PlayerSelectionScript>().sceneNameToLoad = levelName;
        // Then tells the playerInputManager to load this level, and unload the other one
        playerInputManager.GetComponent<PlayerSelectionScript>().Play();
    }
}
