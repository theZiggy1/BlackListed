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

    public bool moveToRoom; // If true, we move to a room and the camera is static, if false then we move to the FollowPlayer object

    private Transform targetObject;
    private bool doLerp;

    // Start is called before the first frame update
    void Start()
    {
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
}
