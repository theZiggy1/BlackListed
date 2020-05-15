using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraFollowObject;

    [SerializeField]
    [Tooltip("This is the place we'll move the camera to")]
    private Transform cameraTargetObject;

    [SerializeField]
    [Tooltip("Should we move the camera's position, or let it do its own thing?")]
    private bool moveCameraPosition = true;

    [SerializeField]
    [Tooltip("Should we move only the camera's rotation, and let it still follow players?")]
    private bool moveCameraRotation;
    [SerializeField]
    private Vector3 cameraRotation;

    [SerializeField]
    [Tooltip("Should we move the camera's rotation, and give it an offset, and let it still follow players?")]
    private bool moveCameraRotationAndOffset;
    [SerializeField]
    private Vector3 cameraOffset;

    [Space(10)]

    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private bool changeCameraMoveSpeed;
    [SerializeField]
    private float cameraMoveSpeed;
    [SerializeField]
    private bool changeCameraRotSpeed;
    [SerializeField]
    private float cameraRotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraFollowObject = GameObject.FindGameObjectWithTag("CameraFollowObject");

        if (cameraFollowObject == null)
        {
            Debug.Log("Trigger can't find cameraFollowObject!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // If a player has touched us
        if (other.CompareTag("Player"))
        {
            // Set the camera to go to where we want it to go
            // Do this by moving the cameraFollowObject
            if (moveCameraPosition)
            {
                cameraFollowObject.GetComponent<CameraFollowObjectScript>().MoveObjectPublic(cameraTargetObject);
            }
            else if (moveCameraRotation) // Set the camera to change rotation but still follow the players
            {
                //cameraFollowObject.GetComponent<CameraFollowObjectScript>().MoveObjectPublic(cameraTargetObject.rotation);
                cameraFollowObject.GetComponent<CameraFollowObjectScript>().MoveObjectPublic(Quaternion.Euler(cameraRotation));
            }
            else if (moveCameraRotationAndOffset) // Set the camera to change rotation, and have an offset, but still follow the players
            {
                //-8.17
                //cameraFollowObject.GetComponent<CameraFollowObjectScript>().MoveObjectPublic(cameraTargetObject.rotation, cameraOffset);
                cameraFollowObject.GetComponent<CameraFollowObjectScript>().MoveObjectPublic(Quaternion.Euler(cameraRotation), cameraOffset);
            }
            else // Set the camera to follow the players again
            {
                cameraFollowObject.GetComponent<CameraFollowObjectScript>().MoveObjectPublic(true);
            }

            if (changeCameraRotSpeed)
            {
                mainCamera.GetComponent<CameraMoveScript>().ChangeRotSpeed(cameraRotSpeed);
            }
            if (changeCameraMoveSpeed)
            {
                mainCamera.GetComponent<CameraMoveScript>().ChangeMoveSpeed(cameraMoveSpeed);
            }
        }
    }
}
