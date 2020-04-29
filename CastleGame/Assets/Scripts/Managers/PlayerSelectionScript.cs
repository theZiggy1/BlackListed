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

    // This is temporary
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

    // Moves the spawn points to the correct locations in the world
    private void ReadyUpSpawnPoints()
    {
        // This will temporarily set the active scene to be the current level, so we can set up spawnpoints
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneNameToLoad));

        // Resets the levelSpawnPoints array so that each level we get the spawnpoints as fresh
        levelSpawnPoints = null;
        //levelSpawnPoints = new GameObject[0];

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
            //Instantiate(playerPrefab, transform.position, transform.rotation);
            //Instantiate(playerPrefab, spawnPoints[spawnIndex].transform.position, spawnPoints[spawnIndex].transform.rotation);

            // Spawns the relevant character, for the player that is currently being spawned
            //Instantiate(playerPrefabs[characterID], spawnPoints[spawnIndex].transform.position, spawnPoints[spawnIndex].transform.rotation);

            GameObject playerInst = Instantiate(playerPrefabs[characterID], spawnPoints[spawnIndex].transform.position, spawnPoints[spawnIndex].transform.rotation);
            spawnedInPlayers[playerID] = playerInst; // Used to keep track of our players

            // Loop through each of the playerInst's children
            foreach (Transform child in playerInst.transform)
            {
                // Find child object with tag PlayerSubObject
                if (child.CompareTag("PlayerSubObject"))
                {
                    // Loop through each of the child's children
                    foreach (Transform secondChild in child)
                    {
                        // Find child object with tag PlayerSubObject - this has the renderer on it
                        if (secondChild.CompareTag("PlayerMeshObject"))
                        {
                            playerChildMats = secondChild.GetComponent<Renderer>().materials;

                            //playerMats[0].color = Color.black;
                            //playerMats[0].SetColor("_MainColor", Color.black);

                            // This will change the material at position 0 to show what player we are
                            // e.g. character's hair colour will change to red if they're player1
                            playerChildMats[0] = PlayerMaterials[playerID];

                            secondChild.GetComponent<Renderer>().materials = playerChildMats;
                        }
                    }
                }
            }

            //Material[] playerMats = playerInst.GetComponent<Renderer>().materials;

            //playerMats[0].color = Color.black;

            //playerInst.GetComponent<Renderer>().materials = playerMats;

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
        //SceneManager.LoadScene(sceneNameToLoad, LoadSceneMode.Additive);
        //SceneManager.LoadSceneAsync(sceneNameToLoad, LoadSceneMode.Additive);
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
        }
        if (sceneNameToLoad == "Level 2")
        {
            RenderSettings.skybox = skyboxes[1];
            RenderSettings.customReflection = reflectionCubemaps[1];
        }
        if (sceneNameToLoad == "Level 3")
        {
            RenderSettings.skybox = skyboxes[2];
            RenderSettings.customReflection = reflectionCubemaps[2];
        }
        if (sceneNameToLoad == "Level 4")
        {
            RenderSettings.skybox = skyboxes[3];
            RenderSettings.customReflection = reflectionCubemaps[3];
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
            //loadingProgress = Mathf.Clamp01(loadingOperation.progress / 0.9f);
            loadingProgress = (loadingOperation.progress / 0.9f);
            Debug.Log("Loading progress: " + loadingProgress);

            loadingBar.value = loadingProgress;
            //loadingBar.value = (Mathf.Clamp01(loadingOperation.progress / 0.9f));

            //// Once we've loaded
            //if (loadingProgress >= 1f)
            //{
            //    // Hide the loading screen
            //    loadingScreenObject.SetActive(false);

            //    Debug.Log("Level should be loaded now");

            //    // Now that the scene is loaded in, we can load the rest of the stuff we need to
            //    LoadLevel();
            //}

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
}
