using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObjectScript : MonoBehaviour
{
    [SerializeField]
    private Vector3 originalPosition; // The original position of the door

    [SerializeField]
    private Transform transformToMoveTo; // Will be an object in the world that we lerp towards

    [SerializeField]
    private float moveSpeed; // How quickly we raise and lower

    [SerializeField]
    [Tooltip("Set to true if we want the door to lower at the start")]
    private bool startsLowered;

    // Start is called before the first frame update
    void Start()
    {
        // Finds our position at the start
        originalPosition = transform.position;

        // Lowers the door at the start if it's set to
        if (startsLowered)
        {
            LowerDoor();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Doors get raised when we enter a room
    public void RaiseDoor()
    {
        // Lerps between transformToMoveTo and original position
        Vector3.Lerp(transformToMoveTo.position, originalPosition, Time.deltaTime * moveSpeed);
    }

    // Doors get lowered when we've finished the wave
    public void LowerDoor()
    {
        // Lerps between original position and transformToMoveTo
        Vector3.Lerp(originalPosition, transformToMoveTo.position, Time.deltaTime * moveSpeed);
    }
}
