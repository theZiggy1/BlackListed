﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Farran Holmes s1712383
/// </summary>
public class WinScreenManager : MonoBehaviour
{
    [SerializeField]
    private string sceneNameMainMenu;

    [SerializeField]
    [Tooltip("The actual canvas Win Screen object")]
    private GameObject winScreenObject;
    [SerializeField]
    [Tooltip("The canvas that has the Ian Intro on it")]
    private GameObject ianIntroScreenObject;

    // Start is called before the first frame update
    void Start()
    {
        // Make sure the win screen doesn't show when we're playing
        DisableWinScreen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableIanIntroScreen()
    {
        ianIntroScreenObject.SetActive(true);
    }
    public void DisableIanIntroScreen()
    {
        ianIntroScreenObject.SetActive(false);
    }

    // Get another thing to call this function when you win the game
    public void EnableWinScreen()
    {
        // Shows the Pause canvas
        winScreenObject.SetActive(true);
        // Enables us to recieve gamepad UI input
        GetComponent<UIGamepadScript>().enabled = true;
    }
    public void DisableWinScreen()
    {
        // Hides the Pause canvas
        winScreenObject.SetActive(false);
        // Disables us from recieving gamepad UI input
        GetComponent<UIGamepadScript>().enabled = false;
    }

    // The 'Quit' button on the end screen will call this function
    public void QuitToMainMenu()
    {
        // Load the Main Menu scene
        SceneManager.LoadScene(sceneNameMainMenu);
    }
}
