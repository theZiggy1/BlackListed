using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObjectScript : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;

    [Space(10)]

    [SerializeField]
    private Transform originalTarget; // This is probably going to be the player follow child object
    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Vector3 rotation;
    [SerializeField]
    private bool hasOffset;
    [SerializeField]
    private bool hasMoved;

    // Start is called before the first frame update
    void Start()
    {
        targetObject = originalTarget;

        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasMoved)
        {
            MoveObject();
            //hasMoved = true;

            for (int i = 0; i < gameManager.GetComponent<GameManagerScript>().numPlayers; i++)
            {
                // Sets each player to know the transform of this camera
                // This is used so that the player can move relative to the camera
                gameManager.GetComponent<GameManagerScript>().currentPlayers[i].GetComponent<PlayerControllerOldInput>().SetCamera(transform);
            }
        }

        // If our target has moved, then we should move again
        if (transform != targetObject.transform)
        {
            hasMoved = false;
        }
        
    }

    private void MoveObject()
    {
        if (hasOffset)
        {
            transform.position = targetObject.position + offset;
        }
        else
        {
            transform.position = targetObject.position;
        }
        transform.rotation = targetObject.rotation;

    }

    // Allows other things to set where we go
    public void MoveObjectPublic(Transform newTarget)
    {
        targetObject = newTarget;
        hasOffset = false;
        hasMoved = false;
    }

    // Allows other things to set us back to our original postion
    public void MoveObjectPublic(bool goBackToOriginalPosition)
    {
        if (goBackToOriginalPosition)
        {
            targetObject = originalTarget;
            originalTarget.GetComponent<FollowPlayerChildScript>().ResetPosition();
            hasOffset = false;
            hasMoved = false;
        }
    }

    // If we want to change the rotation of the camera, but still let it follow the players
    public void MoveObjectPublic(Quaternion newRotation)
    {
        targetObject = originalTarget;
        originalTarget.GetComponent<FollowPlayerChildScript>().ResetPosition();
        targetObject.position = originalTarget.position;
        rotation = newRotation.eulerAngles;
        targetObject.rotation = Quaternion.Euler(rotation);
        hasOffset = false;
        hasMoved = false;
    }

    // If we want to change the rotation of the camera, but still let it follow the players, with an offset
    public void MoveObjectPublic(Quaternion newRotation, Vector3 newOffset)
    {
        targetObject = originalTarget;
        originalTarget.GetComponent<FollowPlayerChildScript>().ResetPosition();
        targetObject.position = originalTarget.position;
        rotation = newRotation.eulerAngles;
        targetObject.rotation = Quaternion.Euler(rotation);
        offset = newOffset;
        hasOffset = true;
        hasMoved = false;
    }

}
