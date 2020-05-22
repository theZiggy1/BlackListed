using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenManagerScript : MonoBehaviour
{
    [SerializeField]
    private string sceneNameMainMenu;

    [SerializeField]
    [Tooltip("Gets set by other things, used to say if the pause screen will appear")]
    private bool pauseScreenEnabled;

    [SerializeField]
    [Tooltip("The actual canvas Pause Screen object")]
    private GameObject pauseScreenObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnablePauseScreen()
    {
        // Shows the Pause canvas
        pauseScreenObject.SetActive(true);
        // Enables us to recieve gamepad UI input
        GetComponent<UIGamepadScript>().enabled = true;
    }
    public void DisablePauseScreen()
    {
        // Hides the Pause canvas
        pauseScreenObject.SetActive(false);
        // Disables us from recieving gamepad UI input
        GetComponent<UIGamepadScript>().enabled = false;
    }

    public void PauseGame()
    {
        EnablePauseScreen();
        // Makes the time scale go to 0.01, so time barely passes
        // This is basically like stopping it, but still allows update to happen
        // So that we can still navigate the menu, as it requires update to work
        //Time.timeScale = 0.01f;
        Time.timeScale = 0f;

        Debug.Log("Paused game! Time scale set to 0!");
    }
    public void ResumeGame()
    {
        DisablePauseScreen();
        // Makes the time scale go back to 1, the default value
        Time.timeScale = 1;

        Debug.Log("Resumed game! Time scale set to 1!");
    }

    // The 'Quit' button on the end screen will call this function
    public void QuitToMainMenu()
    {
        // Make sure to resume before we quit
        ResumeGame();

        // Load the Main Menu scene
        SceneManager.LoadScene(sceneNameMainMenu);

        // Unload the Game Over screen scene
        SceneManager.UnloadSceneAsync("GameOverScreen");
    }
}
