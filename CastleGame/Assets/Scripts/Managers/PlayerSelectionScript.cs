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
    [SerializeField]
    private string sceneNameToLoad; // There's probably a better way to do this

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


    // Player UI elements - will be set to hidden at start, then appear when loaded into level
    [SerializeField]
    private GameObject player1UI;
    [SerializeField]
    private GameObject player2UI;
    [SerializeField]
    private GameObject player3UI;
    [SerializeField]
    private GameObject player4UI;


    // This will be set to true once we've loaded the level
    private bool levelLoaded;


    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");

        // We can just drag them in in the inspector, but getting with tags saves time
        player1UI = GameObject.FindGameObjectWithTag("Player1UI");
        player2UI = GameObject.FindGameObjectWithTag("Player2UI");
        player3UI = GameObject.FindGameObjectWithTag("Player3UI");
        player4UI = GameObject.FindGameObjectWithTag("Player4UI");
        // Deactivate all the UI at the start
        player1UI.SetActive(false);
        player2UI.SetActive(false);
        player3UI.SetActive(false);
        player4UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Moved this to the PlayerSelectManagerScript, in the PlayerSelectScene
        //if (!levelLoaded)
        //{
        //    // If any controller presses start, then we start the game (will be probably be changed to a countdown or wait for all joined players to ready up first)
        //    if (Input.GetButtonDown("Joy1ButtonStart") || Input.GetButtonDown("Joy2ButtonStart") || Input.GetButtonDown("Joy3ButtonStart") || Input.GetButtonDown("Joy4ButtonStart"))
        //    {
        //        // Load the level
        //        Play();
        //    }

        //    levelLoaded = true;
        //}
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

        // Reactivate all the player UI when we load the level
        player1UI.SetActive(true);
        player2UI.SetActive(true);
        player3UI.SetActive(true);
        player4UI.SetActive(true);

        // Annoyingly this is read only
        //GetComponent<PlayerInputManager>().maxPlayerCount

        // Load the scene additively, so that our currently open scene stays open
        //SceneManager.LoadScene(gameScene.name, LoadSceneMode.Additive);

        // Load the level we're gonna go to
        //SceneManager.LoadScene("Level1", LoadSceneMode.Additive);
        SceneManager.LoadScene(sceneNameToLoad, LoadSceneMode.Additive);

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
