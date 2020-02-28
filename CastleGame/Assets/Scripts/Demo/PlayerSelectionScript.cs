using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// PlayerSelectionScript
/// By Farran Holmes
/// 18/2/20
/// </summary>


public class PlayerSelectionScript : MonoBehaviour
{
    //[SerializeField]
    //private string level1SceneName; // There's probably a better way to do this

    [SerializeField]
    private GameObject playerPrefab; // The player we are spawning
    [SerializeField]
    private int numOfPlayers; // We could technically get the number of players from the GameManager, but we don't need to

    [SerializeField]
    private GameObject[] spawnPoints;
    [SerializeField]
    private int spawnIndex; // Used to say what spawnpoint we are going to place the current player at

    [SerializeField]
    private GameObject GameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Queues our player as 'joined to the game' ready for spawning
    public void JoinPlayer()
    {
        numOfPlayers++;
        GameManager.GetComponent<GameManagerScript>().playersQueued = numOfPlayers;
    }

    // Actually spawns the player in the world
    public void SpawnPlayer()
    {
        if (numOfPlayers < 4)
        {
            //Instantiate(playerPrefab, transform.position, transform.rotation);
            Instantiate(playerPrefab, spawnPoints[spawnIndex].transform.position, spawnPoints[spawnIndex].transform.rotation);
            spawnIndex++;

            //numOfPlayers++;
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

        // Load the level we're gonna go to
        SceneManager.LoadScene("Level1", LoadSceneMode.Additive);

        // Set the active scene to be PlayerScene, so that our players get instantiated there
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("PlayerScene"));
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint"); // These ideally *should* be in the level scene but at the moment they're in the player scene

        for (int i = 0; i < numOfPlayers; i++)
        {
            SpawnPlayer();
        }

        // Unload the player select scene
        SceneManager.UnloadSceneAsync("PlayerSelectScene");
    }
}
