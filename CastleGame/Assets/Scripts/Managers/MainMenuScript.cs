using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Farran Holmes
/// s1712383
/// </summary>

public class MainMenuScript : MonoBehaviour
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

    public void PlayGame()
    {
        Debug.Log("Play game!");
        
        SceneManager.LoadScene(sceneNameToLoad, LoadSceneMode.Single);
    }
}
