using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///TestTriggerArea Script
/// Catherine Burns
/// 27/02/2020
/// </summary>
public class TestTriggerArea : MonoBehaviour
{
    [SerializeField] GameObject battleArea;
    [SerializeField] GameObject[] removeableWalls;

    string PLAYER_TAG = "Player";

    Vector3 direction;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PLAYER_TAG)
        {
            direction = other.transform.position - transform.position;
            // if the dot product is 1, behind collider
            if(Vector3.Dot(transform.forward, direction) > 0)
            {
                Debug.Log("Back");
                // disable the removeable walls
                foreach(GameObject wall in removeableWalls)
                {
                    wall.SetActive(false);
                }
            }
            // if the dot product is -1, in front of collider
            if(Vector3.Dot(transform.forward, direction) < 0)
            {
                Debug.Log("Front");
                // enable the removeable walls
                foreach (GameObject wall in removeableWalls)
                {
                    wall.SetActive(true);
                }
            }
        }
    }

    // Disable the arena when leaving
    private void OnTriggerExit(Collider other)
    {
        battleArea.SetActive(false);
    }
}
