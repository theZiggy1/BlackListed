using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizardAbility : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> Players;
    public string PLAYER_TAG = "Player";
    void Start()
    {
        Players = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        foreach(GameObject player in Players)
        {
            //Add Health
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == PLAYER_TAG)
        {
            Players.Remove(other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PLAYER_TAG)
        {
            Players.Add(other.gameObject);
        }
    }

}
