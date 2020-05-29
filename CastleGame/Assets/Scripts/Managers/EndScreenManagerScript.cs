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
        // Allows us to wait a second before taking any input
        StartCoroutine(WaitBeforeController());

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



    }

    public void LoadNextLevel()
    {


        // Set the playerInputManager to have the correct scene to load next
        playerInputManager.GetComponent<PlayerSelectionScript>().sceneNameToLoad = sceneNameNext;
        // Set the playerInputManager to have the correct scene to unload
        playerInputManager.GetComponent<PlayerSelectionScript>().sceneNameToUnload = sceneNamePrevious;
        // Then tell the playerInputManager to load that scene
        playerInputManager.GetComponent<PlayerSelectionScript>().Play();



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

    IEnumerator WaitBeforeController()
    {
        yield return new WaitForSeconds(0.25f);

        // Enables our gamepad UI script, after the time has expired
        GetComponent<UIGamepadScript>().enabled = true;
    }
}
