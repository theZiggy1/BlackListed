using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoomCameraMoveTo : MonoBehaviour
{
    //This script moved the camera to a room when you entered it, and was changed when the camera was moved to follw the player. 
    // Start is called before the first frame update
    string PLAYER_TAG = "Player";
    [SerializeField] GameObject Camera;
    [SerializeField] Transform RoomLocation;
    public bool CameraControl = false;
    public bool isWallEnter = false;
    public bool isRoomEnter = false;
    [SerializeField] GameObject BattleArea;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == PLAYER_TAG)
        {
            // If we want the camera to move to a specific location in the room
            if (CameraControl) { Camera.GetComponent<CameraMoveScript>().setCameraMoveTo(RoomLocation); }
            // If we instead want the camera to just follow the players
            else { Camera.GetComponent<CameraMoveScript>().setCameraFollowPlayers(); }

            if (isWallEnter)
            {
                BattleArea.GetComponent<ComponentsToEnableandDisable>().WallComponentsTrigger();
            }
            if (isRoomEnter)
            {
                BattleArea.GetComponent<ComponentsToEnableandDisable>().RoomComponentsTrigger();

                BattleArea.GetComponent<ComponentsToEnableandDisable>().WallComponentsTrigger();

            }


           

          
        }
    }


}
