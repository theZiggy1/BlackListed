using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject door;
    [SerializeField]
    [Tooltip("Do we want to open the door, or close it?")]
    private bool openDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openDoor)
            {
                // Opens the door
                door.GetComponent<DoorObjectScript>().LowerDoor();
            }
            else
            {
                // Closes the door
                door.GetComponent<DoorObjectScript>().RaiseDoor();
            }
        }
    }
}
