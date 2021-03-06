﻿using System.Collections;
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
    public string sceneNameToLoad; // There's probably a better way to do this
    public string sceneNameToUnload;

    [SerializeField]
    private GameObject[] playerPrefabs; // Array containing the characters that the player can choose from
    [SerializeField]
    private int numOfPlayers; // We could technically get the number of players from the GameManager, but we don't need to
    public int[] playerCharIDs; // Array containing the IDs of the characters that each player picked
    [SerializeField]
    private Material[] playerChildMats; // Contains the materials for the child object of the player character

    [SerializeField]
    private Material[] PlayerMaterials; // Array containing materials that colour code our players

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

    private AsyncOperation loadingOperation;
    private float loadingProgress;

    private AsyncOperation unloadingOperation;
    private float unloadingProgress;

    [SerializeField]
    private GameObject loadingScreenObject; // Gets set to active once we're loading, and set to false once we've finished loading
    [SerializeField]
    private Slider loadingBar;
    [SerializeField]
    private Text loadingBarText;


    [SerializeField]
    GameObject[] levelSpawnPoints;

    [SerializeField]
    private bool loadingFromPlayerSelect;

    [SerializeField]
    private GameObject[] spawnedInPlayers; // The players that are currently spawned in

    [SerializeField]
    private Material[] skyboxes;
    [SerializeField]
    private Cubemap[] reflectionCubemaps;

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

        loadingFromPlayerSelect = true;
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

    // Moves the spawn points to the correct locations in the world
    private void ReadyUpSpawnPoints()
    {
        // This will temporarily set the active scene to be the current level, so we can set up spawnpoints
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneNameToLoad));

        // Resets the levelSpawnPoints array so that each level we get the spawnpoints as fresh
        levelSpawnPoints = null;

        // Create a new array containing all the spawn points
        levelSpawnPoints = GameObject.FindGameObjectsWithTag("PlayerSpawnPoint");

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // Moves the spawnpoint to the correct point in the world
            spawnPoints[i].transform.position = new Vector3(levelSpawnPoints[i].transform.position.x, levelSpawnPoints[i].transform.position.y, levelSpawnPoints[i].transform.position.z);
        }
    }

    // Actually spawns the player in the world
    public void SpawnPlayer(int characterID, int playerID)
    {
        if (numOfPlayers < 4)
        {
            // Spawns the relevant character, for the player that is currently being spawned
            GameObject playerInst = Instantiate(playerPrefabs[characterID], spawnPoints[spawnIndex].transform.position, spawnPoints[spawnIndex].transform.rotation);

            // Changes the colour of our clothing according to which player we are
            GameObject clothingPiece = playerInst.GetComponent<PlayerControllerOldInput>().clothingPiece;
            playerChildMats = clothingPiece.GetComponent<Renderer>().materials;
            playerChildMats[0] = PlayerMaterials[playerID];
            clothingPiece.GetComponent<Renderer>().materials = playerChildMats;

            // Sets each player to know us
            playerInst.GetComponent<PlayerControllerOldInput>().playerInputManager = gameObject;

            // Sets the player so that they know what character they are
            playerInst.GetComponent<PlayerControllerOldInput>().characterID = characterID;

            spawnedInPlayers[playerID] = playerInst; // Used to keep track of our players
            
            spawnIndex++;

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




        StartCoroutine(LoadSceneAsynchronously());

    }

    // Done so that we only load stuff in once the level is fully loaded in
    private void LoadLevel()
    {
        // Then change our skybox to the relevant one for the level we're on
        UpdateSkybox();

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint"); // These ideally *should* be in the level scene but at the moment they're in the player scene
        
        // It's safe to do this now, as we know that the level has definitely been loaded
        ReadyUpSpawnPoints();

        // Set the active scene to be PlayerScene, so that our players get instantiated there
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("PlayerScene"));

        

        // If we're loading in to the level 'fresh' from the player select (e.g. it's level 1)
        if (loadingFromPlayerSelect)
        {
            spawnedInPlayers = new GameObject[numOfPlayers]; // News the spawnedInPlayers array to be the length of numOfPlayers

            for (int i = 0; i < numOfPlayers; i++)
            {
                // For each of our players, spawn the relevant character, with the relevant colour
                // e.g. if i = 2 then that's player 3, so spawn the character at position 2 of the playerCharIDs array
                SpawnPlayer(playerCharIDs[i], i);

                if (i == 0)
                {
                    player1UI.SetActive(true);
                }
                if (i == 1)
                {
                    player2UI.SetActive(true);
                }
                if (i == 2)
                {
                    player3UI.SetActive(true);
                }
                if (i == 3)
                {
                    player4UI.SetActive(true);
                }

            }

            // Once players are spawned in:
            // Tell the FollowPlayer object how many players there are
            GameObject followPlayerObject = GameObject.FindGameObjectWithTag("FollowObject");
            followPlayerObject.GetComponent<FollowPlayerScript>().numberOfPlayers = numOfPlayers;
            // Set the FollowPlayer object's playerObjects array to be the players
            followPlayerObject.GetComponent<FollowPlayerScript>().playerObjects = GameManager.GetComponent<GameManagerScript>().currentPlayers;
            // Tell the FollowPlayer object to start following the players
            followPlayerObject.GetComponent<FollowPlayerScript>().startFollowing = true;


            // Unload the player select scene
            SceneManager.UnloadSceneAsync("PlayerSelectScene");

            loadingFromPlayerSelect = false;
        }
        else // If we're loading in from another level previously
        {
            // We need to move the players to the relevant spawns, kinda like respawning them
            for (int i = 0; i < spawnedInPlayers.Length; i++)
            {
                // Moves the relevant player to the relevant spawn point
                spawnedInPlayers[i].transform.position = spawnPoints[i].transform.position;
                // Sets the relevant player to have 0 velocity when they spawn in
                spawnedInPlayers[i].GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            }
        }

    }

    // Updates our skybox to be the relevant one for the current level
    private void UpdateSkybox()
    {
        if (sceneNameToLoad == "Level1")
        {
            RenderSettings.skybox = skyboxes[0];
            RenderSettings.customReflection = reflectionCubemaps[0];
            RenderSettings.fog = false;
        }
        if (sceneNameToLoad == "Level 2")
        {
            RenderSettings.skybox = skyboxes[1];
            RenderSettings.customReflection = reflectionCubemaps[1];
            RenderSettings.fog = false;
        }
        if (sceneNameToLoad == "Level 3")
        {
            RenderSettings.skybox = skyboxes[2];
            RenderSettings.customReflection = reflectionCubemaps[2];
            RenderSettings.fog = false;
        }
        if (sceneNameToLoad == "Level 4")
        {
            RenderSettings.skybox = skyboxes[3];
            RenderSettings.customReflection = reflectionCubemaps[3];
            RenderSettings.fog = false;
        }
    }

    private IEnumerator LoadSceneAsynchronously()
    {
        loadingScreenObject.SetActive(true);

        // Loads our scene asynchronously in the background
        loadingOperation = SceneManager.LoadSceneAsync(sceneNameToLoad, LoadSceneMode.Additive);
        
        // While the loading isn't complete
        while (!loadingOperation.isDone)
        {
            // Transforms the 0-0.9 value of loading into a 0-1 value

            loadingProgress = (loadingOperation.progress / 0.9f);
            Debug.Log("Loading progress: " + loadingProgress);

            loadingBar.value = loadingProgress;

            loadingBarText.text = (loadingProgress * 100f).ToString() + "%";

            // Wait till next frame
            yield return null;
        }
        if (loadingOperation.isDone)
        {
            Debug.Log("We've loaded the level now");

            // Hide the loading screen
            loadingScreenObject.SetActive(false);

            if (!loadingFromPlayerSelect)
            {
                // Start the unloading coroutine
                StartCoroutine(UnloadSceneAsynchronously());
            }
            else // We don't have to unload the previous level so we can go straight to this
            {
                // Now that the scene is loaded in, we can load the rest of the stuff we need to
                LoadLevel();
            }
            
        }
    }

    private IEnumerator UnloadSceneAsynchronously()
    {
        // Unload the current scene
        unloadingOperation = SceneManager.UnloadSceneAsync(sceneNameToUnload);

        while (!unloadingOperation.isDone)
        {
            Debug.Log("Unloading the previous level: " + unloadingOperation.progress);
            // Wait till next frame
            yield return null;
        }
        if (unloadingOperation.isDone) // If we've finished unloading the level
        {
            // Then we can safely load the next level
            LoadLevel();
        }
    }

    // Players will call this each time one of them dies, if they're all dead, then we show the Game Over screen
    public void CheckGameOver()
    {
        bool showGameOver = false;

        foreach(GameObject player in spawnedInPlayers)
        {
            if (!player.GetComponent<EntityScript>().isDead)
            {
                Debug.Log("A player is still alive");

                showGameOver = false;

                // Breaks out of the loop, as at least one of our players is still alive
                break;
            }
            else
            {
                showGameOver = true;
            }
        }

        if (showGameOver)
        {
            // Loads the Game Over screen
            SceneManager.LoadSceneAsync("GameOverScreen", LoadSceneMode.Additive);
        }
    }


}
