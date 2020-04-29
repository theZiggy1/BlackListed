using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManagerScript : MonoBehaviour
{
    // We'll grab these from the previous scene's EndLevelTrigger
    [SerializeField]
    private string sceneNameNext;
    [SerializeField]
    private string sceneNamePrevious; // The previous level, that we'll unload

    // This we can just set ourselves, as we always know the name of the main menu
    [SerializeField]
    private string sceneNameMainMenu;

    [SerializeField]
    private GameObject playerInputManager; // Need to find this to tell it to load the next scene
    [SerializeField]
    private GameObject endLevelTrigger; // Need to find this to tell us what scenes to load/unload

    // Start is called before the first frame update
    void Start()
    {
        // When our EndScreen scene loads in, find the playerInputManager from the previous scene
        playerInputManager = GameObject.FindGameObjectWithTag("PlayerInputManager");

        // When our EndScreen scene loads in, find the endLevelTrigger from the previous scene
        endLevelTrigger = GameObject.FindGameObjectWithTag("EndLevelTrigger");
        // Once we've found it, set our scene names
        sceneNameNext = endLevelTrigger.GetComponent<EndLevelTriggerScript>().nextLevelName;
        sceneNamePrevious = endLevelTrigger.GetComponent<EndLevelTriggerScript>().currentLevelName;
    }

    // Update is called once per frame
    void Update()
    {
        //// If any controller presses start, then we start the next level (will be probably be changed to a countdown or wait for all joined players to ready up first)
        //if (Input.GetButtonDown("Joy1ButtonStart") || Input.GetButtonDown("Joy2ButtonStart") || Input.GetButtonDown("Joy3ButtonStart") || Input.GetButtonDown("Joy4ButtonStart"))
        //{
        //    LoadNextLevel();
        //}
    }

    public void LoadNextLevel()
    {
        // This will load the named scene, which should be the end screen scene
        //SceneManager.LoadScene(sceneNameNext, LoadSceneMode.Additive);

        // Set the playerInputManager to have the correct scene to load next
        playerInputManager.GetComponent<PlayerSelectionScript>().sceneNameToLoad = sceneNameNext;
        // Set the playerInputManager to have the correct scene to unload
        playerInputManager.GetComponent<PlayerSelectionScript>().sceneNameToUnload = sceneNamePrevious;
        // Then tell the playerInputManager to load that scene
        playerInputManager.GetComponent<PlayerSelectionScript>().Play();

        // Unload the current scene
        //SceneManager.UnloadSceneAsync(sceneNamePrevious);

        // Unload the end screen scene
        SceneManager.UnloadSceneAsync("EndScreen");
    }

    // The 'Quit' button on the end screen will call this function
    public void QuitToMainMenu()
    {
        // Load the Main Menu scene
        SceneManager.LoadScene(sceneNameMainMenu);

        // Unload the end screen scene
        SceneManager.UnloadSceneAsync("EndScreen");
    }
}
