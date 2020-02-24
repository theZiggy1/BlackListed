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
    private GameObject playerPrefab; // The player we are spawning

    [SerializeField]
    private int numOfPlayers; // We could technically get the number of players from the GameManager, but we don't need to
    

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
        if (numOfPlayers < 4)
        {
            Instantiate(playerPrefab, transform.position, transform.rotation);

            numOfPlayers++;
        }
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
