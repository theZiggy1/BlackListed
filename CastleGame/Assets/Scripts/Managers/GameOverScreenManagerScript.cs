using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenManagerScript : MonoBehaviour
{
    [SerializeField]
    private string sceneNameMainMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // The 'Quit' button on the end screen will call this function
    public void QuitToMainMenu()
    {
        // Load the Main Menu scene
        SceneManager.LoadScene(sceneNameMainMenu);

        // Unload the Game Over screen scene
        SceneManager.UnloadSceneAsync("GameOverScreen");
    }
}
