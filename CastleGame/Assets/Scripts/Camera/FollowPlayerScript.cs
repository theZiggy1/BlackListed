using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Farran Holmes s1712383
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


    public GameObject[] playerObjects;

    // Start is called before the first frame update
    void Start()
    {
        // Find the Game Manager
        gameManager = GameObject.FindGameObjectWithTag(GAMEMANAGER_TAG);

    }

    // Update is called once per frame
    void Update()
    {



        if (startFollowing)
        {


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


            transform.position = new Vector3(averageX, averageY, averageZ);



        }
        else
        {
            Debug.Log("PlayerFollow object isn't following anything yet!");
        }
    }
}
