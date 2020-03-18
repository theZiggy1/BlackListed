using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// By Farran Holmes
/// s1712383
/// </summary>
public class EndLevelTriggerScript : MonoBehaviour
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
        
    }

    private void ShowEndScreen()
    {
        // This will load the named scene, which should be the end screen scene
        SceneManager.LoadScene(sceneNameToLoad, LoadSceneMode.Additive);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If a player enters the trigger box, we show the end screen
        if (other.gameObject.CompareTag("Player"))
        {
            ShowEndScreen();
        }        
    }
}
