using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Farran Holmes
/// 19/2/20
/// </summary>

public class FollowPlayerScript : MonoBehaviour
{
    private string GAMEMANAGER_TAG = "GameManager";
    private string MAINCAM_TAG = "MainCamera";
    private GameObject gameManager;

    // Camera variables
    [SerializeField]
    [Tooltip("The minimum aperture (zoom) of the camera")]
    private float minAperture;
    [SerializeField]
    [Tooltip("The maximum aperture (zoom) of the camera")]
    private float maxAperture;

    //[SerializeField]
    //private GameObject mainCamera; // Our main camera
    //[SerializeField]
    //private GameObject camMoveObject; // The object attached to us that the camera lerps towards

    public int numberOfPlayers; // The number of players we have, will be set by the PlayerSelectManager once we load the level
    public bool startFollowing; // Set to false at the start, gets set to true once the level has loaded in

    [SerializeField]
    private Vector3 targetVector; // The average vector of all of the players in the game
    [SerializeField]
    private float averageX; // The average of all the X components of the vectors
    [SerializeField]
    private float averageY; // The average of all the Y components of the vectors
    [SerializeField]
    private float averageZ; // The average of all the Z components of the vectors

    [SerializeField]
    private float averageXPlusP1; // The average of all the X components of the vectors
    [SerializeField]
    private float averageYPlusP1; // The average of all the Y components of the vectors
    [SerializeField]
    private float averageZPlusP1; // The average of all the Z components of the vectors

    //[SerializeField]
    //private Vector3 player1Pos; // The vector3 position of player 1
    //[SerializeField]
    //private Vector3 player2Pos; // The vector3 position of player 2
    //[SerializeField]
    //private Vector3 player3Pos; // The vector3 position of player 3
    //[SerializeField]
    //private Vector3 player4Pos; // The vector3 position of player 4

    public GameObject[] playerObjects;

    // Start is called before the first frame update
    void Start()
    {
        // Find the Game Manager
        gameManager = GameObject.FindGameObjectWithTag(GAMEMANAGER_TAG);

        // Set up the playerPositions array
        //player1Pos = gameManager.GetComponent<GameManagerScript>().currentPlayers[0].
        // Get the GameManager (or something) to set this array up once we load in

        // Don't need to find the main camera anymore, as now the camera movement is camera centric
        // Find Main Camera
        //mainCamera = GameObject.FindGameObjectWithTag(MAINCAM_TAG);
    }

    // Update is called once per frame
    void Update()
    {
        if (startFollowing)
        {
            //!!!Need to make this Dynamic!!!

            // To make it dynamic, do a for loop that incrementally adds on each Vec3 value, then divides by how many players there are, to get an average value
            // We should have been told how many players there are to follow
            for (int i = 0; i < numberOfPlayers; i++)
            {
                //targetVector

                // Tot up our x, y, and z values for each player
                averageX += playerObjects[i].transform.position.x;
                averageY += playerObjects[i].transform.position.y;
                averageZ += playerObjects[i].transform.position.z;
            }

            // Then divide those values by the number of players, to get an average
            // Not sure why I have to add 1 to it but it fixes it
            averageX = averageX / (numberOfPlayers + 1);
            averageY = averageY / (numberOfPlayers + 1);
            averageZ = averageZ / (numberOfPlayers + 1);

            

            // This is hardcoded for 2 players, come back to this to make it dynamic for however many players there are
            //Vector3 player1Pos = gameManager.GetComponent<GameManagerScript>().currentPlayers[0].transform.position;
            //Vector3 player2Pos = gameManager.GetComponent<GameManagerScript>().currentPlayers[1].transform.position;

            //averageXPlusP1 = player1Pos.x + ((averageX - player1Pos.x) / 2);
            //averageYPlusP1 = player1Pos.y + ((averageY - player1Pos.y) / 2);
            //averageZPlusP1 = player1Pos.z + ((averageZ - player1Pos.z) / 2);

            //averageXPlusP1 = averageX + ((player1Pos.x - averageX) / 2);
            //averageYPlusP1 = averageY + ((player1Pos.y - averageY) / 2);
            //averageZPlusP1 = averageZ + ((player1Pos.z - averageZ) / 2);

            //averageXPlusP1 = averageX - player1Pos.x;
            //averageYPlusP1 = averageY - player1Pos.y;
            //averageZPlusP1 = averageZ - player1Pos.z;

            //transform.position = new Vector3((player1Pos.x + (player2Pos.x - player1Pos.x) / 2),
            //                                  (player1Pos.y + (player2Pos.y - player1Pos.y) / 2),
            //                                  (player1Pos.z + (player2Pos.z - player1Pos.z) / 2));

            transform.position = new Vector3(averageX, averageY, averageZ);


            // Camera FOV change
            //mainCamera.GetComponent<Camera>().fieldOfView

            //Vector3 playerDistanceDifference = new Vector3((player1Pos.x + (player2Pos.x - player1Pos.x) / 2),
            //                                                (player1Pos.y + (player2Pos.y - player1Pos.y) / 2),
            //                                                (player1Pos.z + (player2Pos.z - player1Pos.z) / 2));

            //mainCamera.GetComponent<Camera>().fieldOfView = 60 + ((playerDistanceDifference.x + playerDistanceDifference.y + playerDistanceDifference.z) / 3);
            //mainCamera.transform.position = new Vector3 (-70 - ((playerDistanceDifference.x + playerDistanceDifference.y + playerDistanceDifference.z) / 3), mainCamera.transform.position.y, mainCamera.transform.position.z);
            //camMoveObject.transform.position = new Vector3(-20 - ((playerDistanceDifference.x + playerDistanceDifference.y + playerDistanceDifference.z) / 3), camMoveObject.transform.position.y, camMoveObject.transform.position.z);

            //mainCamera.GetComponent<CameraMoveScript>().

        }
        else
        {
            Debug.Log("PlayerFollow object isn't following anything yet!");
        }
    }
}
