using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenManager : MonoBehaviour
{
    [SerializeField]
    private string sceneNameMainMenu;

    [SerializeField]
    [Tooltip("The actual canvas Win Screen object")]
    private GameObject winScreenObject;

    // Start is called before the first frame update
    void Start()
    {
        // Make sure the win screen doesn't show when we're playing
        DisableWinScreen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Get another thing to call this function when you win the game
    public void EnableWinScreen()
    {
        // Shows the Pause canvas
        winScreenObject.SetActive(true);
        // Enables us to recieve gamepad UI input
        GetComponent<UIGamepadScript>().enabled = true;
    }
    public void DisableWinScreen()
    {
        // Hides the Pause canvas
        winScreenObject.SetActive(false);
        // Disables us from recieving gamepad UI input
        GetComponent<UIGamepadScript>().enabled = false;
    }

    // The 'Quit' button on the end screen will call this function
    public void QuitToMainMenu()
    {
        // Make sure to resume before we quit
        //ResumeGame();

        // Load the Main Menu scene
        SceneManager.LoadScene(sceneNameMainMenu);

        // Unload the Game Over screen scene
        //SceneManager.UnloadSceneAsync("GameOverScreen");
    }
}
