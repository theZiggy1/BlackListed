using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObjectScript : MonoBehaviour
{
    [SerializeField]
    private float originalAudioLevel;
    [SerializeField]
    private float currentAudioLevel;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<AudioSource>() != null)
        {
            originalAudioLevel = GetComponent<AudioSource>().volume;
            currentAudioLevel = originalAudioLevel;
        }
        else
        {
            Debug.Log("We don't have an audio source!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Has to be System.Single as that's what a slider passes in
    // It's effectively the same as a float though
    public void changeAudio(System.Single audioMultiplier)
    {
        if (GetComponent<AudioSource>() != null)
        {
            currentAudioLevel = originalAudioLevel * audioMultiplier;
            GetComponent<AudioSource>().volume = currentAudioLevel;
        }
        else
        {
            Debug.Log("We don't have an audio source!");
        }
    }
}
