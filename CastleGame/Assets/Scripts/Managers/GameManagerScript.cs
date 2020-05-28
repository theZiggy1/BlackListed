using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************
 * Anton Ziegler s1907905
 * ****************/
public class GameManagerScript : MonoBehaviour

{
    public int numPlayers; // This is the number of players actually spawned currently
    public int playersQueued; // This is the number of players queued up for spawning
    public GameObject[] currentPlayers; //This is all currently playing players
    public bool[] isEngaged; //this is whether the player is being attacked by an ememy

    [Space(10)]

    public float sfxAudioLevel;
    public float musicAudioLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
