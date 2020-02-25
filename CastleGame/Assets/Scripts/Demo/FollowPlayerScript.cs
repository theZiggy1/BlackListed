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

    //private GameObject mainCamera; // Our main camera
    [SerializeField]
    private GameObject camMoveObject; // The object attached to us that the camera lerps towards


    // Start is called before the first frame update
    void Start()
    {
        // Find the Game Manager
        gameManager = GameObject.FindGameObjectWithTag(GAMEMANAGER_TAG);

        // Don't need to find the main camera anymore, as now the camera movement is camera centric
        // Find Main Camera
        //mainCamera = GameObject.FindGameObjectWithTag(MAINCAM_TAG);
    }

    // Update is called once per frame
    void Update()
    {
        // This is hardcoded for 2 players, come back to this to make it dynamic for however many players there are
        Vector3 player1Pos = gameManager.GetComponent<GameManagerScript>().currentPlayers[0].transform.position;
        Vector3 player2Pos = gameManager.GetComponent<GameManagerScript>().currentPlayers[1].transform.position;
        
        transform.position = new Vector3((player1Pos.x + (player2Pos.x - player1Pos.x) / 2),
                                          (player1Pos.y + (player2Pos.y - player1Pos.y) / 2),
                                          (player1Pos.z + (player2Pos.z - player1Pos.z) / 2));


        // Camera FOV change
        //mainCamera.GetComponent<Camera>().fieldOfView

        Vector3 playerDistanceDifference = new Vector3((player1Pos.x + (player2Pos.x - player1Pos.x) / 2),
                                                        (player1Pos.y + (player2Pos.y - player1Pos.y) / 2),
                                                        (player1Pos.z + (player2Pos.z - player1Pos.z) / 2));

        //mainCamera.GetComponent<Camera>().fieldOfView = 60 + ((playerDistanceDifference.x + playerDistanceDifference.y + playerDistanceDifference.z) / 3);
        //mainCamera.transform.position = new Vector3 (-70 - ((playerDistanceDifference.x + playerDistanceDifference.y + playerDistanceDifference.z) / 3), mainCamera.transform.position.y, mainCamera.transform.position.z);
        camMoveObject.transform.position = new Vector3(-20 - ((playerDistanceDifference.x + playerDistanceDifference.y + playerDistanceDifference.z) / 3), camMoveObject.transform.position.y, camMoveObject.transform.position.z);
    }
}
