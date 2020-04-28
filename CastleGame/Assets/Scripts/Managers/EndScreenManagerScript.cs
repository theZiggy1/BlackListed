using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManagerScript : MonoBehaviour
{
    [SerializeField]
    private string sceneNameNext;
    [SerializeField]
    private string sceneNamePrevious; // The previous level, that we'll unload

    [SerializeField]
    private GameObject playerInputManager; // Need to find this to tell it to load the next scene

    // Start is called before the first frame update
    void Start()
    {
        // When our EndScreen scene loads in, find the playerInputManager from the previous scene
        playerInputManager = GameObject.FindGameObjectWithTag("PlayerInputManager");
    }

    // Update is called once per frame
    void Update()
    {
        // If any controller presses start, then we start the next level (will be probably be changed to a countdown or wait for all joined players to ready up first)
        if (Input.GetButtonDown("Joy1ButtonStart") || Input.GetButtonDown("Joy2ButtonStart") || Input.GetButtonDown("Joy3ButtonStart") || Input.GetButtonDown("Joy4ButtonStart"))
        {
            LoadNextLevel();
        }
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
}
