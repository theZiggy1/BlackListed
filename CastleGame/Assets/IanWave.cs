﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// By Anton Ziegler s1907905
/// and Farran Holmes s1712383
/// </summary>

public class IanWave : Wave
{
    [SerializeField]
    private GameObject winManager;
    [SerializeField]
    private AudioSource audioSourceMusic; // The AudioSource playing the background music
    [SerializeField]
    private AudioClip ianMusic;


    public override void SpawnEnemies()
    {
        base.SpawnEnemies();
        // Stops the cutscene when the enemies spawn in
        winManager.GetComponent<WinScreenManager>().DisableIanIntroScreen();
        // Set the AudioSource to have the ian music - and play it
        if (ianMusic != null)
        {
            audioSourceMusic.clip = ianMusic;
            audioSourceMusic.Play();
        }

        //Need to also unlock the players' movement so they can now move around again
        if (gameObject != null)
        {
            foreach (GameObject player in gameManager.GetComponent<GameManagerScript>().currentPlayers)
            {
                if (player != null)
                {
                    player.GetComponent<PlayerControllerOldInput>().isTumbling = false;
                }
            }
        }

        //Spawn the single boss.  
        GameObject FinalBoss = GameObject.Instantiate(ArrayofEnemies[0], spawnLocations[0].position, spawnLocations[0].rotation);
        FinalBoss.GetComponent<EnemyVarScript>().SpawningInfo(Spawner, gameManager, Random.Range(0, gameManager.GetComponent<GameManagerScript>().numPlayers));
    }

    public override void OnStartWave()
    {
        base.OnStartWave();
        //Will need to play the cutscene here for the boss spawning in 

        // Find the WinManager object, so we can set Ian's canvas to appear
        winManager = GameObject.FindGameObjectWithTag("WinManager");
        // Set Ian's canvas to active
        winManager.GetComponent<WinScreenManager>().EnableIanIntroScreen();
        // Stop playing background music
        audioSourceMusic.Stop();

        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        //Need to also lock the players' movement so they can't move around till the wave has spawned
        if (gameManager != null)
        {
            foreach (GameObject player in gameManager.GetComponent<GameManagerScript>().currentPlayers)
            {
                if (player != null)
                {
                    player.GetComponent<PlayerControllerOldInput>().isTumbling = true;
                }
            }
        }


    }

}
