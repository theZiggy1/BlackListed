using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObjectScript : MonoBehaviour
{
    [Tooltip("So that the music and SFX volumes know what to apply to")]
    public bool isMusic;
    [SerializeField]
    private float originalAudioLevel;
    [SerializeField]
    private float currentAudioLevel;

    [Space(5)]

    //[SerializeField]
    //private GameObject audioManager;
    [SerializeField]
    private GameObject gameManager;

    [SerializeField]
    private bool isInMainMenu;


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

        // In the main menu the game manager isn't there, so the slider sets our audio level instead
        if (!isInMainMenu)
        {
            // Game manager has the music and sfx audio levels on it
            gameManager = GameObject.FindGameObjectWithTag("GameManager");
            if (gameManager != null)
            {
                if (isMusic)
                {
                    changeAudio(gameManager.GetComponent<GameManagerScript>().musicAudioLevel);
                }
                else
                {
                    changeAudio(gameManager.GetComponent<GameManagerScript>().sfxAudioLevel);
                }
            }
            else
            {
                Debug.Log("Can't find game manager!");
            }
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
