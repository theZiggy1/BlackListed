using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRoomScript : MonoBehaviour
{
    [SerializeField]
    private GameObject battleArea;

    [Space(15)]

    // This script will 'lock' a room once all players have entered
    [SerializeField]
    private GameObject[] doorLockColliders; // The colliders that appear when a room is 'locked'
    [SerializeField]
    [Tooltip("Used to say which doors are locked at the start, corresponds with the doorLockColliders array")]
    private bool[] doorsLockedAtStart;

    [Space(15)]

    [SerializeField]
    private GameObject gameManager; // Find this so we can know how many players there are

    [SerializeField]
    private int numOfPlayers; // The number of players in the game, used to tell when all players are in the battle arena

    //[SerializeField]
    //private bool[] playersEntered; // Array to keep track of which players have entered and which are yet to enter, or have left
    //// I already know this is going to cause a rare bug with the way players/controllers are asigned, so I'll fix that next

    [SerializeField]
    private int playersEntered; // Used to keep track of how many players have entered the trigger

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        numOfPlayers = gameManager.GetComponent<GameManagerScript>().numPlayers;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!enabled) { return; }
    //    if (other.gameObject.tag == PLAYER_TAG)
    //    {
    //        Debug.Log("Player " + other.GetComponent<PlayerControllerOldInput>().playerID + " has entered the arena");

    //        // If a player enters the trigger area, then 'enters' again, this actually means they'll be leaving the battle area,
    //        if (playersEntered[other.GetComponent<PlayerControllerOldInput>().playerNum] == false) // If a player hasn't yet entered
    //        {
    //            // We've now entered, so set our relevant bool in the array to true
    //            playersEntered[other.GetComponent<PlayerControllerOldInput>().playerNum] = true;
    //        }
    //        if (playersEntered[other.GetComponent<PlayerControllerOldInput>().playerNum] == true) // If a player has entered before
    //        {
    //            // We're now exiting, so set our relevant bool in the array to true
    //            playersEntered[other.GetComponent<PlayerControllerOldInput>().playerNum] = false;
    //        }



    //        // Need to wait till *all* players that are in the game have entered before doing this
    //        if (battleArea != null)
    //        {
    //            battleArea.GetComponent<WaveSpawning>().playersHaveEntered();
    //        }
    //        this.gameObject.GetComponent<TriggerArea>().enabled = false;
    //    }
    //}

    private void StartWave()
    {
        if (battleArea != null)
        {
            battleArea.GetComponent<WaveSpawning>().playersHaveEntered();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If a player enters our trigger
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("A player has entered the arena");
            playersEntered++;
        }

        // If *all* the players have entered
        if (playersEntered == numOfPlayers)
        {
            Debug.Log("All players have entered, starting wave");
            StartWave();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If a player leaves our trigger
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("A player has left the arena");
            playersEntered--;
        }
    }
}
