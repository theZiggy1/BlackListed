using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizardAbility : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> Players;
    public string PLAYER_TAG = "Player";
    public float gainAmount = -1.0f;
    float timeBetweenGain = 1.0f;
    float resetTime = 1.0f;
    void Start()
    {
        Players = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

        timeBetweenGain -= Time.deltaTime;
        if (timeBetweenGain <= 0)
        {
            timeBetweenGain = resetTime;
            foreach (GameObject player in Players)
            {
                //Add Health
                player.GetComponent<EntityScript>().HealingRift(1.0f);
            }
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
