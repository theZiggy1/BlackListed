using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************
 * Anton Ziegler s1907905
 * ****************/
public class LockRoomScript : MonoBehaviour
{
    [SerializeField]
    private GameObject battleArea;

    [Space(15)]

    // This script will 'lock' a room once all players have entered
    [SerializeField]
    private GameObject[] doorLockColliders; // The colliders that appear when a room is 'locked'
    //[SerializeField]
    //[Tooltip("Used to say which doors are locked at the start, corresponds with the doorLockColliders array")]
    //private bool[] doorsLockedAtStart;
    [SerializeField]
    private GameObject[] doorObjects;


    [Space(15)]

    [SerializeField]
    private GameObject gameManager; // Find this so we can know how many players there are

    [SerializeField]
    private int numOfPlayers; // The number of players in the game, used to tell when all players are in the battle arena

    public int playersEntered; // Used to keep track of how many players have entered the trigger

    [SerializeField]
    private GameObject cameraFollowObject;
    [SerializeField]
    [Tooltip("This is the position we set in our room for the camera to go to, and the angle as well")]
    private Transform cameraPositionObject;
    [SerializeField]
    [Tooltip("If set to true, our camera will move towards the cameraPositionObject")]
    private bool moveCameraPosition;

    [SerializeField]
    private bool waveNotSpawned = true;

    [SerializeField]
    [Tooltip("How long we give the players to enter the room, after this has expired, players are instead teleported into the room")]
    private float playersEnteredTimer = 10f;

    [SerializeField]
    [Tooltip("The points that the four players get teleported to, if they don't enter the room in time")]
    private Transform[] teleportPoints;

    [SerializeField]
    [Tooltip("The players in the scene, found at runtime")]
    private GameObject[] players;

    public bool[] playersEnteredArray; // If e.g. player 2 enters, then element 1 is changed to true, if they leave, it's changed to false again

    [SerializeField]
    private bool coroutineStarted;
    //[SerializeField]
    //private bool coroutineRunning;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        if (gameManager != null)
        {
            numOfPlayers = gameManager.GetComponent<GameManagerScript>().numPlayers;
            // Gets our current players, so that we can teleport the players if needs be
            players = gameManager.GetComponent<GameManagerScript>().currentPlayers;

            // Initialises the playersEnteredArray to be the correct length
            playersEnteredArray = new bool[numOfPlayers];
        }
        else
        {
            Debug.Log("Can't find the Game Manager!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // numOfPlayers may equal 0 for a few frames when the first level loads in, due to the way scenes are loaded asyncrhonously
        if (numOfPlayers == 0)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager");

            if (gameManager != null)
            {
                numOfPlayers = gameManager.GetComponent<GameManagerScript>().numPlayers;
                // Gets our current players, so that we can teleport the players if needs be
                players = gameManager.GetComponent<GameManagerScript>().currentPlayers;

                // Initialises the playersEnteredArray to be the correct length
                playersEnteredArray = new bool[numOfPlayers];
            }
            else
            {
                Debug.Log("Can't find the Game Manager! - 0 players!");
            }
        }
    }

    private void StartWave()
    {
        if (waveNotSpawned)
        {
            if (battleArea != null)
            {
                // Tells the battle area to start the waves of enemies
                battleArea.GetComponent<WaveSpawning>().playersHaveEntered();

                // Locks the room
                LockRoom();

                // Now the wave has spawned, so we set this to false
                waveNotSpawned = false;
            }
        }
    }


    // This will activate the room's colliders, and raise the doors
    private void LockRoom()
    {
        Debug.Log("Locking room!");
        // Activate all the colliders
        foreach (GameObject colliderBox in doorLockColliders)
        {
            colliderBox.SetActive(true);
        }

        // Raise the doors
        foreach (GameObject door in doorObjects)
        {
            door.GetComponent<DoorObjectScript>().RaiseDoor();
        }

        if (moveCameraPosition)
        {
            cameraFollowObject.GetComponent<CameraFollowObjectScript>().MoveObjectPublic(cameraPositionObject);
        }
    }

    // Deactivates the room's colliders, and lowers the doors
    public void UnlockRoom()
    {
        Debug.Log("Unlocking room!");
        // Deactivate all the colliders
        foreach (GameObject colliderBox in doorLockColliders)
        {
            colliderBox.SetActive(false);
        }

        // Lower the doors
        foreach (GameObject door in doorObjects)
        {
            door.GetComponent<DoorObjectScript>().LowerDoor();
        }

        if (moveCameraPosition)
        {
            cameraFollowObject.GetComponent<CameraFollowObjectScript>().MoveObjectPublic(true);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            // We only need to do this if not all the players have entered yet
            if (playersEntered != numOfPlayers)
            {
                //// If a player enters our trigger
                //if (other.gameObject.CompareTag("Player"))
                //{
                    // Only need to do the parts below, if the player hasn't already entered before,
                    // keep track of who has and hasn't entered

                    // If the current player hasn't entered the arena before
                    if (playersEnteredArray[other.GetComponent<PlayerControllerOldInput>().playerNum] == false)
                    {
                        Debug.Log("Player " + other.GetComponent<PlayerControllerOldInput>().playerID + " has entered the arena");
                        playersEntered++;
                    }
                    playersEnteredArray[other.GetComponent<PlayerControllerOldInput>().playerNum] = true;

                    // If we only have one player we don't have to do any teleporting
                    // of the remaining players
                    if (numOfPlayers > 1)
                    {
                        if (!coroutineStarted)
                        {
                            Debug.Log("Starting countdown");
                            StartCoroutine(EnteredRoomTimer());

                            coroutineStarted = true;
                        }
                    }
                //}
            }

            // If *all* the players have entered
            if (playersEntered == numOfPlayers)
            {
                Debug.Log("All players have entered, starting wave");
                StartWave();

                Debug.Log("Stopping countdown, as all players have entered");
                StopCoroutine(EnteredRoomTimer());
            }
        }
    }

    // Teleports all the players into the room
    private void TeleportPlayersToRoom()
    {
        Debug.Log("Teleporting players to room");

        for (int i = 0; i < numOfPlayers; i++)
        {
            // 'Teleports' the player
            players[i].transform.position = teleportPoints[i].position;

            // If a player is dead
            if (players[i].GetComponent<EntityScript>().isDead)
            {
                // Then revive that player
                players[i].GetComponent<EntityScript>().Revive();
            }
        }
    }

    private IEnumerator EnteredRoomTimer()
    {
        //coroutineRunning = true;
        // Waits for timer to be up
        yield return new WaitForSeconds(playersEnteredTimer);

        //coroutineRunning = false;

        if (playersEntered != numOfPlayers)
        {
            // Then teleports the players into the room
            TeleportPlayersToRoom();

            // Also makes sure that the wave starts
            StartWave();
        }
        else
        {
            Debug.Log("All players have already entered, so no need to teleport players");
        }
    }
}
