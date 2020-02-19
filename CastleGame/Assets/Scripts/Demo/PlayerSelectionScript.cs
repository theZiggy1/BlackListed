using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

/// <summary>
/// PlayerSelectionScript
/// By Farran Holmes
/// 18/2/20
/// </summary>


public class PlayerSelectionScript : MonoBehaviour
{
    //[SerializeField]
    //private GameObject gameScene;

    [SerializeField]
    private GameObject playerPrefab;

    private int numOfPlayers;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer()
    {
        //GameObject playerInst;
        //playerInst = Instantiate(playerPrefab, transform.position, transform.rotation);
        // Can't set .user, it's read only
        //playerInst.GetComponent<PlayerInput>().user = 
        // Not sure how to get custom spawning of players to work with assigning controllers

        //PlayerInput.Instantiate(playerPrefab, controlScheme: "DefaultControls", device: Keyboard.current);
        //PlayerInput.Instantiate(playerPrefab, numOfPlayers, controlScheme: "DefaultControls", 0);
        //PlayerInputManager.JoinPlayer(numOfPlayers, 0, controlScheme: "DefaultControls", InputDevice.all);

        numOfPlayers++;
    }

    private void OnPlayerJoined()
    {
        Debug.Log("A player has joined!");
    }

    // Called when we press the play button
    public void Play()
    {
        Debug.Log("Play game!");

        // Annoyingly this is read only
        //GetComponent<PlayerInputManager>().maxPlayerCount

        // Load the scene additively, so that our currently open scene stays open
        //SceneManager.LoadScene(gameScene.name, LoadSceneMode.Additive);
        SceneManager.LoadScene("DemoLevel", LoadSceneMode.Additive);
    }
}
