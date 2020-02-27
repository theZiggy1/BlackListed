using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoomCameraMoveTo : MonoBehaviour
{
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
            if (CameraControl) { Camera.GetComponent<CameraMoveScript>().setCameraMoveTo(RoomLocation); }

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
