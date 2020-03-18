using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// By Farran Holmes
/// s1712383
/// </summary>
public class PlayerSelectManagerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject playerInputObject; // The object containing our player input script

    [SerializeField]
    private Image player1JoinedImage;
    [SerializeField]
    private Image player2JoinedImage;
    [SerializeField]
    private Image player3JoinedImage;
    [SerializeField]
    private Image player4JoinedImage;

    private void Awake()
    {
        SceneManager.LoadScene("PlayerScene", LoadSceneMode.Additive);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInputObject = GameObject.FindGameObjectWithTag("PlayerInputManager");

        player1JoinedImage.enabled = false;
        player2JoinedImage.enabled = false;
        player3JoinedImage.enabled = false;
        player4JoinedImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Joy1ButtonA"))
        {
            player1JoinedImage.enabled = true;
            playerInputObject.GetComponent<PlayerSelectionScript>().JoinPlayer();
        }
        if (Input.GetButtonDown("Joy2ButtonA"))
        {
            player2JoinedImage.enabled = true;
            playerInputObject.GetComponent<PlayerSelectionScript>().JoinPlayer();
        }
        if (Input.GetButtonDown("Joy3ButtonA"))
        {
            player3JoinedImage.enabled = true;
            playerInputObject.GetComponent<PlayerSelectionScript>().JoinPlayer();
        }
        if (Input.GetButtonDown("Joy4ButtonA"))
        {
            player4JoinedImage.enabled = true;
            playerInputObject.GetComponent<PlayerSelectionScript>().JoinPlayer();
        }
    }
}
