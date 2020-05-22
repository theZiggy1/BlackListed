﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPassingScript : MonoBehaviour
{
    [SerializeField]
    private GameObject audioManager;
    [SerializeField]
    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        gameManager.GetComponent<GameManagerScript>().sfxAudioLevel = audioManager.GetComponent<AudioManagerScript>().sfxAudioLevel;
        gameManager.GetComponent<GameManagerScript>().musicAudioLevel = audioManager.GetComponent<AudioManagerScript>().musicAudioLevel;

        Destroy(audioManager);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}