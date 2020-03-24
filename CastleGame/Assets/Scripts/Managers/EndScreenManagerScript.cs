using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManagerScript : MonoBehaviour
{
    [SerializeField]
    private string sceneNameToLoad;

    // Start is called before the first frame update
    void Start()
    {
        
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

    private void LoadNextLevel()
    {
        // This will load the named scene, which should be the end screen scene
        SceneManager.LoadScene(sceneNameToLoad, LoadSceneMode.Additive);

        // Unload the end screen scene
        SceneManager.UnloadSceneAsync("EndScreen");
    }
}
