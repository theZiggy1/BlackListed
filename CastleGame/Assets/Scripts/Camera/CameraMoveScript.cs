using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;
    
    [Space(10)]

    public Transform roomTargetObject; // This is the object attached to the room that the camera moves to - will be set by the room once the players enter
    [SerializeField]
    private Transform followPlayerObject; // This is the FollowPlayer object that moves between the players
    [SerializeField]
    private float moveSpeed = 5f; // The movement speed of the camera
    [SerializeField]
    private float rotateSpeed = 2f;
    [SerializeField]
    private float moveSpeedOriginal = 5f; // The movement speed of the camera
    [SerializeField]
    private float rotateSpeedOriginal = 2f;

    public bool moveToRoom; // Tells the camera to move to the current room's camera location, if it's in that mode

    //public bool followingPlayers; // Tells the camera to follow the players instead of going to a room's camera location

    private Transform targetObject;
    private bool doLerp;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject followObject = GameObject.FindGameObjectWithTag("FollowObject");

        //if (followObject != null)
        //{
        //    foreach (Transform child in followObject.transform)
        //    {
        //        if (child.CompareTag("FollowObjectChild"))
        //        {
        //            followPlayerObject = child;
        //        }
        //    }
        //}
        //else
        //{
        //    Debug.Log("Camera can't find FollowPlayer object!");
        //}

        //targetObject = followPlayerObject;

        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        if (gameManager != null)
        {
            //foreach (GameObject player in gameManager.GetComponent<GameManagerScript>().currentPlayers)
            //{
            //    // Sets each player to know the transform of this camera
            //    // This is used so that the player can move relative to the camera
            //    player.GetComponent<PlayerControllerOldInput>().SetCamera(transform);
            //}

            for (int i = 0; i < gameManager.GetComponent<GameManagerScript>().numPlayers; i++)
            {
                // Sets each player to know the transform of this camera
                // This is used so that the player can move relative to the camera
                gameManager.GetComponent<GameManagerScript>().currentPlayers[i].GetComponent<PlayerControllerOldInput>().SetCamera(transform);
            }
        }
        else
        {
            Debug.Log("MainCamera can't find the GameManager!");
        }

        followPlayerObject = GameObject.FindGameObjectWithTag("CameraFollowObject").transform;
        
        if (followPlayerObject != null)
        {
            targetObject = followPlayerObject;
        }
        else
        {
            Debug.Log("targetObject is null! Camera can't find the CameraFollowObject!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        // If we are instructed to move to a room
        if (moveToRoom)
        {
            // Then set our target object to that room
            targetObject = roomTargetObject;
            
            if (Vector3.Distance(transform.position, targetObject.position) < 0.01f)
            {
                doLerp = false; // We've gotten within an acceptable range, so stop the camera
                moveSpeed = moveSpeedOriginal;
                rotateSpeed = rotateSpeedOriginal;
            }
            else
            {
                doLerp = true; // And set doLerp to true so we move there
            }

        }
        else // If we aren't instructed to move to a room
        {
            // Then set our target object to the FollowPlayer object
            targetObject = followPlayerObject;
            doLerp = true; // And set doLerp to true so we move there
        }

        if (doLerp)
        {
            MoveCamera();          
        }

    }

    private void MoveCamera()
    {
        // Moves us smoothly towards the target object
        transform.position = Vector3.MoveTowards(transform.position, targetObject.position, moveSpeed * Time.deltaTime);

        // Change camera angle - looks in the same direction as the target object (its forward vector)
        //transform.LookAt(targetObject.forward);
        //transform.rotation = targetObject.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetObject.rotation, rotateSpeed * Time.deltaTime);
    }

    public void setCameraMoveTo(Transform RoomLocationToMoveTo)
    {
        roomTargetObject = RoomLocationToMoveTo;
        moveToRoom = true;
        doLerp = true;
    }

    public void setCameraFollowPlayers()
    {
        moveToRoom = false;
    }

    public void ChangeMoveSpeed(float newMoveSpeed)
    {
        moveSpeed = newMoveSpeed;
    }
    public void ChangeRotSpeed(float newRotSpeed)
    {
        rotateSpeed = newRotSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the camera has a collider on it
        if (GetComponent<BoxCollider>() != null)
        {
            if (other.gameObject != null)
            {
                if (other.gameObject.CompareTag("CameraHideable"))
                {
                    // If we hit a thing, and it has a renderer on it
                    if (other.gameObject.GetComponent<Renderer>() != null)
                    {
                        // Then disable that renderer, so that the camera can see through it
                        other.gameObject.GetComponent<Renderer>().enabled = false;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the camera has a collider on it
        if (GetComponent<BoxCollider>() != null)
        {
            if (other.gameObject != null)
            {
                if (other.gameObject.CompareTag("CameraHideable"))
                {
                    // If we have stopped hitting a thing, and it has a renderer on it
                    if (other.gameObject.GetComponent<Renderer>() != null)
                    {
                        // Then enable that renderer again
                        other.gameObject.GetComponent<Renderer>().enabled = true;
                    }
                }
            }
        }
    }

}
