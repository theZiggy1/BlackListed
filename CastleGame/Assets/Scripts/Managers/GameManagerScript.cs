using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManagerScript : MonoBehaviour
{
    public int numPlayers; // This is the number of players actually spawned currently
    public int playersQueued; // This is the number of players queued up for spawning
    public GameObject[] currentPlayers;
    public bool[] isEngaged;

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
