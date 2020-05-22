using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public float sfxAudioLevel;
    public float musicAudioLevel;

    // Start is called before the first frame update
    void Start()
    {
        // Don't destroy us on scene load, so that we can go into all the scenes
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSFXAudioLevel(System.Single audioLevel)
    {
        sfxAudioLevel = audioLevel;
    }

    public void ChangeMusicAudioLevel(System.Single audioLevel)
    {
        musicAudioLevel = audioLevel;
    }
}
