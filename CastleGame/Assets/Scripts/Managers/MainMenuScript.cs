using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Farran Holmes
/// s1712383
/// </summary>

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private string sceneNameToLoad;

    private AsyncOperation loadingOperation;
    private float loadingProgress;

    private AsyncOperation unloadingOperation;
    private float unloadingProgress;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        Debug.Log("Play game!");

        //SceneManager.LoadScene(sceneNameToLoad, LoadSceneMode.Single);

        // Loads our scene asynchronously in the background
        //loadingOperation = SceneManager.LoadSceneAsync(sceneNameToLoad, LoadSceneMode.Single);
        //loadingOperation.allowSceneActivation = false;

        StartCoroutine(LoadSceneAsynchronously());
    }

    private IEnumerator LoadSceneAsynchronously()
    {
        //loadingScreenObject.SetActive(true);

        // Loads our scene asynchronously in the background
        loadingOperation = SceneManager.LoadSceneAsync(sceneNameToLoad, LoadSceneMode.Single);
        loadingOperation.allowSceneActivation = true;

        // While the loading isn't complete
        while (!loadingOperation.isDone)
        {
            // Transforms the 0-0.9 value of loading into a 0-1 value
            //loadingProgress = Mathf.Clamp01(loadingOperation.progress / 0.9f);
            loadingProgress = (loadingOperation.progress / 0.9f);
            Debug.Log("Loading progress: " + loadingProgress);

            //loadingBar.value = loadingProgress;
            //loadingBar.value = (Mathf.Clamp01(loadingOperation.progress / 0.9f));

            // Wait till next frame
            yield return null;
        }
        if (loadingOperation.isDone)
        {
            Debug.Log("We've loaded the level now");

            // Hide the loading screen
            //loadingScreenObject.SetActive(false);

            //if (!loadingFromPlayerSelect)
            //{
                // Start the unloading coroutine
                StartCoroutine(UnloadSceneAsynchronously());
            //}
            //else // We don't have to unload the previous level so we can go straight to this
            //{

                // Now that the scene is loaded in, we can load the rest of the stuff we need to
                //LoadLevel();
            //}

        }
    }

    private IEnumerator UnloadSceneAsynchronously()
    {
        // Unload the current scene
        unloadingOperation = SceneManager.UnloadSceneAsync("MainMenuScene");

        while (!unloadingOperation.isDone)
        {
            Debug.Log("Unloading the previous level: " + unloadingOperation.progress);
            // Wait till next frame
            yield return null;
        }
        //if (unloadingOperation.isDone) // If we've finished unloading the level
        //{
            // Then we can safely load the next level
            //LoadLevel();
        //}
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        
#if UNITY_EDITOR
        // If we're using Unity Editor, stop play mode
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // If we're using built game, quit application
        Application.Quit ();
#endif

    }
}
