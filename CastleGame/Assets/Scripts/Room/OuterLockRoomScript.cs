using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/******************
 * Anton Ziegler s1907905
 * Farran Holmes s1712383
 * ****************/
public class OuterLockRoomScript : MonoBehaviour
{
    [SerializeField]
    private GameObject roomLockerInner;

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
        // We need to tell the roomLockerInner to *remove* the player that entered from the list
        // as we're the outer room locker so we remove from the list, the inner one adds to it

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player " + other.GetComponent<PlayerControllerOldInput>().playerID + " has left the arena");

            // If that player was in the room previously, reduce the number of players that have entered by 1
            if (roomLockerInner.GetComponent<LockRoomScript>().playersEnteredArray[other.GetComponent<PlayerControllerOldInput>().playerNum] == true)
            {
                roomLockerInner.GetComponent<LockRoomScript>().playersEntered--;
            }

            // Player is leaving the room, so set their corresponding array element to false
            roomLockerInner.GetComponent<LockRoomScript>().playersEnteredArray[other.GetComponent<PlayerControllerOldInput>().playerNum] = false;
            
        }
    }

}
