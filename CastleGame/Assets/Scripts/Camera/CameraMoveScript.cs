using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
    public Transform roomTargetObject; // This is the object attached to the room that the camera moves to - will be set by the room once the players enter
    [SerializeField]
    private Transform followPlayerObject; // This is the FollowPlayer object that moves between the players
    [SerializeField]
    private float moveSpeed; // The movement speed of the camera

    public bool moveToRoom; // Tells the camera to move to the current room's camera location, if it's in that mode

    //public bool followingPlayers; // Tells the camera to follow the players instead of going to a room's camera location

    private Transform targetObject;
    private bool doLerp;

    // Start is called before the first frame update
    void Start()
    {
        GameObject followObject = GameObject.FindGameObjectWithTag("FollowObject");

        if (followObject != null)
        {
            foreach (Transform child in followObject.transform)
            {
                if (child.CompareTag("FollowObjectChild"))
                {
                    followPlayerObject = child;
                }
            }
        }
        else
        {
            Debug.Log("Camera can't find FollowPlayer object!");
        }

        targetObject = followPlayerObject;
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
        transform.position = Vector3.MoveTowards(transform.position, targetObject.position, moveSpeed * Time.deltaTime);
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

    private void OnTriggerEnter(Collider other)
    {
        // If the camera has a collider on it
        if (GetComponent<BoxCollider>() != null)
        {
            if (other.gameObject != null)
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

    private void OnTriggerExit(Collider other)
    {
        // If the camera has a collider on it
        if (GetComponent<BoxCollider>() != null)
        {
            if (other.gameObject != null)
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
