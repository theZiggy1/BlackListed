using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoopScript : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioObjectOne;
    [SerializeField]
    private AudioSource audioObjectTwo;

    [SerializeField]
    private bool changeAudio;

    // Start is called before the first frame update
    void Start()
    {
        //audioObjectTwo.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeAudio)
        {
            // Once our first audio has finished playing
            if (!audioObjectOne.isPlaying)
            {
                // Then play the second audio object
                audioObjectTwo.enabled = true;
                // And disable the first audio object
                audioObjectOne.enabled = false;
            }
        }
    }

    public void ChangeAudio()
    {
        changeAudio = true;
    }

    public void ChangeAudio(AudioClip audioOne, AudioClip audioTwo)
    {
        audioObjectOne.clip = audioOne;
        audioObjectTwo.clip = audioTwo;

        audioObjectOne.enabled = true;
        audioObjectTwo.enabled = false;

        changeAudio = true;
    }

    public void StopAudio()
    {
        audioObjectOne.enabled = false;
        audioObjectTwo.enabled = false;

        changeAudio = false;
    }


}
