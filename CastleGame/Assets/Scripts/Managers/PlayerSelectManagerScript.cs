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

    // The images that display 'Press A' to join
    [SerializeField]
    private GameObject player1JoinedImage;
    [SerializeField]
    private GameObject player2JoinedImage;
    [SerializeField]
    private GameObject player3JoinedImage;
    [SerializeField]
    private GameObject player4JoinedImage;

    //// The images showing which character has been selected
    //[SerializeField]
    //private Image Player1CharacterImage;
    //[SerializeField]
    //private Image Player2CharacterImage;
    //[SerializeField]
    //private Image Player3CharacterImage;
    //[SerializeField]
    //private Image Player4CharacterImage;

    // The images showing which character has been selected
    [SerializeField]
    private RawImage Player1CharacterImage;
    [SerializeField]
    private RawImage Player2CharacterImage;
    [SerializeField]
    private RawImage Player3CharacterImage;
    [SerializeField]
    private RawImage Player4CharacterImage;

    //!!! For use with Images
    //// Used to store all the images of the characters for the character select
    //[SerializeField]
    //private Sprite[] characterImages;

    //!!!For use with RawImages
    // Used to store all the images of the characters for the character select
    //[SerializeField]
    //private Texture[] characterImages;

    [SerializeField]
    private Texture[] characterImagesP1;
    [SerializeField]
    private Texture[] characterImagesP2;
    [SerializeField]
    private Texture[] characterImagesP3;
    [SerializeField]
    private Texture[] characterImagesP4;

    private bool P1Joined;
    private bool P2Joined;
    private bool P3Joined;
    private bool P4Joined;

    // Bools for moving the images, so we don't scroll through when we hold the stick down
    private bool P1MovedImage;
    private bool P2MovedImage;
    private bool P3MovedImage;
    private bool P4MovedImage;

    private bool loadedNextLevel;

    // These are public so that the player's can read them and get the correct character from them
    // E.g. if P1CharacterID is 3 then Player1 will read that, and change to using Character3
    public int P1CharacterID;
    public int P2CharacterID;
    public int P3CharacterID;
    public int P4CharacterID;

    // This is done so we can colour each character in the select screen, to reflect which player we are
    [SerializeField]
    private GameObject[] characterObjects; // Stores the characters that are displayed for the charcter select
    [SerializeField]
    private Material[] playerMats; // Stores the materials of each player, e.g. P1 = red, P2 = green, etc

    private void Awake()
    {
        SceneManager.LoadScene("PlayerScene", LoadSceneMode.Additive);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInputObject = GameObject.FindGameObjectWithTag("PlayerInputManager");

        // Use to have these appear when you press a, now have it so they disappear when you press a
        //player1JoinedImage.enabled = false;
        //player2JoinedImage.enabled = false;
        //player3JoinedImage.enabled = false;
        //player4JoinedImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!P1Joined)
        {
            if (Input.GetButtonDown("Joy1ButtonA"))
            {
                //player1JoinedImage.enabled = false;
                player1JoinedImage.SetActive(false);
                playerInputObject.GetComponent<PlayerSelectionScript>().JoinPlayer();
                P1Joined = true;

                // Sets us to show char1's portrait
                //Player1CharacterImage.sprite = characterImages[0];
                Player1CharacterImage.texture = characterImagesP1[0];
            }
        }
        if (!P2Joined)
        {
            if (Input.GetButtonDown("Joy2ButtonA"))
            {
                //player2JoinedImage.enabled = false;
                player2JoinedImage.SetActive(false);
                playerInputObject.GetComponent<PlayerSelectionScript>().JoinPlayer();
                P2Joined = true;

                // Sets us to show char1's portrait
                //Player2CharacterImage.sprite = characterImages[0];
                Player2CharacterImage.texture = characterImagesP2[0];
            }
        }
        if (!P3Joined)
        {
            if (Input.GetButtonDown("Joy3ButtonA"))
            {
                //player3JoinedImage.enabled = false;
                player3JoinedImage.SetActive(false);
                playerInputObject.GetComponent<PlayerSelectionScript>().JoinPlayer();
                P3Joined = true;

                // Sets us to show char1's portrait
                //Player3CharacterImage.sprite = characterImages[0];
                Player3CharacterImage.texture = characterImagesP3[0];
            }
        }
        if (!P4Joined)
        {
            if (Input.GetButtonDown("Joy4ButtonA"))
            {
                //player4JoinedImage.enabled = false;
                player4JoinedImage.SetActive(false);
                playerInputObject.GetComponent<PlayerSelectionScript>().JoinPlayer();
                P4Joined = true;

                // Sets us to show char1's portrait
                //Player4CharacterImage.sprite = characterImages[0];
                Player4CharacterImage.texture = characterImagesP4[0];
            }
        }


        // Player 1 character select
        if (P1Joined)
        {
            if (!P1MovedImage) // If we haven't moved images yet
            {
                // -1 is to the right, 1 is to the left
                // So, smaller than -0.5 means we've started moving the stick halfway to the right
                if (Input.GetAxis("Joy1LeftStickHorizontal") < -0.5f)
                {
                    Debug.Log("Move right, to the next image");

                    // Add 1 onto the characterID
                    P1CharacterID++;

                    // If it gets greater than the number of characters we have, reset the value
                    if (P1CharacterID >= characterImagesP1.Length)
                    {
                        P1CharacterID = 0;
                    }

                    // Set the player's image to be the revelant one from the characterImages array
                    //Player1CharacterImage.sprite = characterImages[P1CharacterID];
                    Player1CharacterImage.texture = characterImagesP1[P1CharacterID];

                    //ChangeCharacterMat(characterObjects[P1CharacterID], 0);

                    P1MovedImage = true;
                }
                // So, greater than 0.5 means we've started moving the stick halfway to the left
                if (Input.GetAxis("Joy1LeftStickHorizontal") > 0.5f)
                {
                    Debug.Log("Move left, to the previous image");

                    // Subtract 1 from the characterID
                    P1CharacterID--;

                    // If it gets less than 0, wrap it back round to the number of characters we have
                    if (P1CharacterID < 0)
                    {
                        P1CharacterID = characterImagesP1.Length - 1;
                    }

                    // Set the player's image to be the revelant one from the characterImages array
                    //Player1CharacterImage.sprite = characterImages[P1CharacterID];
                    Player1CharacterImage.texture = characterImagesP1[P1CharacterID];

                    //ChangeCharacterMat(characterObjects[P1CharacterID], 0);

                    P1MovedImage = true;
                }
            }
            // If our stick is back in the centre, then
            if (Input.GetAxis("Joy1LeftStickHorizontal") < 0.25f && Input.GetAxis("Joy1LeftStickHorizontal") > -0.25f)
            {
                // Reset this so we can move images again
                P1MovedImage = false;
            }
        }


        // Player 2 character select
        if (P2Joined)
        {
            if (!P2MovedImage) // If we haven't moved images yet
            {
                // -1 is to the right, 1 is to the left
                // So, smaller than -0.5 means we've started moving the stick halfway to the right
                if (Input.GetAxis("Joy2LeftStickHorizontal") < -0.5f)
                {
                    Debug.Log("Move right, to the next image");

                    // Add 1 onto the characterID
                    P2CharacterID++;

                    // If it gets greater than the number of characters we have, reset the value
                    if (P2CharacterID >= characterImagesP2.Length)
                    {
                        P2CharacterID = 0;
                    }

                    // Set the player's image to be the revelant one from the characterImages array
                    //Player2CharacterImage.sprite = characterImages[P2CharacterID];
                    Player2CharacterImage.texture = characterImagesP2[P2CharacterID];

                    P2MovedImage = true;
                }
                // So, greater than 0.5 means we've started moving the stick halfway to the left
                if (Input.GetAxis("Joy2LeftStickHorizontal") > 0.5f)
                {
                    Debug.Log("Move left, to the previous image");

                    // Subtract 1 from the characterID
                    P2CharacterID--;

                    // If it gets less than 0, wrap it back round to the number of characters we have
                    if (P2CharacterID < 0)
                    {
                        P2CharacterID = characterImagesP2.Length - 1;
                    }

                    // Set the player's image to be the revelant one from the characterImages array
                    //Player2CharacterImage.sprite = characterImages[P2CharacterID];
                    Player2CharacterImage.texture = characterImagesP2[P2CharacterID];

                    P2MovedImage = true;
                }
            }
            // If our stick is back in the centre, then
            if (Input.GetAxis("Joy2LeftStickHorizontal") < 0.25f && Input.GetAxis("Joy2LeftStickHorizontal") > -0.25f)
            {
                // Reset this so we can move images again
                P2MovedImage = false;
            }
        }


        // Player 3 character select
        if (P3Joined)
        {
            if (!P3MovedImage) // If we haven't moved images yet
            {
                // -1 is to the right, 1 is to the left
                // So, smaller than -0.5 means we've started moving the stick halfway to the right
                if (Input.GetAxis("Joy3LeftStickHorizontal") < -0.5f)
                {
                    Debug.Log("Move right, to the next image");

                    // Add 1 onto the characterID
                    P3CharacterID++;

                    // If it gets greater than the number of characters we have, reset the value
                    if (P3CharacterID >= characterImagesP3.Length)
                    {
                        P3CharacterID = 0;
                    }

                    // Set the player's image to be the revelant one from the characterImages array
                    //Player3CharacterImage.sprite = characterImages[P3CharacterID];
                    Player3CharacterImage.texture = characterImagesP3[P3CharacterID];

                    P3MovedImage = true;
                }
                // So, greater than 0.5 means we've started moving the stick halfway to the left
                if (Input.GetAxis("Joy3LeftStickHorizontal") > 0.5f)
                {
                    Debug.Log("Move left, to the previous image");

                    // Subtract 1 from the characterID
                    P3CharacterID--;

                    // If it gets less than 0, wrap it back round to the number of characters we have
                    if (P3CharacterID < 0)
                    {
                        P3CharacterID = characterImagesP3.Length - 1;
                    }

                    // Set the player's image to be the revelant one from the characterImages array
                    //Player3CharacterImage.sprite = characterImages[P3CharacterID];
                    Player3CharacterImage.texture = characterImagesP3[P3CharacterID];

                    P3MovedImage = true;
                }
            }
            // If our stick is back in the centre, then
            if (Input.GetAxis("Joy3LeftStickHorizontal") < 0.25f && Input.GetAxis("Joy3LeftStickHorizontal") > -0.25f)
            {
                // Reset this so we can move images again
                P3MovedImage = false;
            }
        }


        // Player 4 character select
        if (P4Joined)
        {
            if (!P4MovedImage) // If we haven't moved images yet
            {
                // -1 is to the right, 1 is to the left
                // So, smaller than -0.5 means we've started moving the stick halfway to the right
                if (Input.GetAxis("Joy4LeftStickHorizontal") < -0.5f)
                {
                    Debug.Log("Move right, to the next image");

                    // Add 1 onto the characterID
                    P4CharacterID++;

                    // If it gets greater than the number of characters we have, reset the value
                    if (P4CharacterID >= characterImagesP4.Length)
                    {
                        P4CharacterID = 0;
                    }

                    // Set the player's image to be the revelant one from the characterImages array
                    //Player4CharacterImage.sprite = characterImages[P4CharacterID];
                    Player4CharacterImage.texture = characterImagesP4[P4CharacterID];

                    P4MovedImage = true;
                }
                // So, greater than 0.5 means we've started moving the stick halfway to the left
                if (Input.GetAxis("Joy4LeftStickHorizontal") > 0.5f)
                {
                    Debug.Log("Move left, to the previous image");

                    // Subtract 1 from the characterID
                    P4CharacterID--;

                    // If it gets less than 0, wrap it back round to the number of characters we have
                    if (P4CharacterID < 0)
                    {
                        P4CharacterID = characterImagesP4.Length - 1;
                    }

                    // Set the player's image to be the revelant one from the characterImages array
                    //Player4CharacterImage.sprite = characterImages[P4CharacterID];
                    Player4CharacterImage.texture = characterImagesP4[P4CharacterID];

                    P4MovedImage = true;
                }
            }
            // If our stick is back in the centre, then
            if (Input.GetAxis("Joy4LeftStickHorizontal") < 0.25f && Input.GetAxis("Joy4LeftStickHorizontal") > -0.25f)
            {
                // Reset this so we can move images again
                P4MovedImage = false;
            }
        }


        // If any controller presses start, then we start the game (will be probably be changed to a countdown or wait for all joined players to ready up first)
        if (P1Joined || P2Joined || P3Joined || P4Joined)
        {
            if (Input.GetButtonDown("Joy1ButtonStart") || Input.GetButtonDown("Joy2ButtonStart") || Input.GetButtonDown("Joy3ButtonStart") || Input.GetButtonDown("Joy4ButtonStart"))
            {
                if (!loadedNextLevel)
                {

                    // Find the PlayerInputManager
                    GameObject playerInputManager = GameObject.FindGameObjectWithTag("PlayerInputManager");

                    // Tell the PlayerInputManager what charIDs each player has picked
                    playerInputManager.GetComponent<PlayerSelectionScript>().playerCharIDs[0] = P1CharacterID;
                    playerInputManager.GetComponent<PlayerSelectionScript>().playerCharIDs[1] = P2CharacterID;
                    playerInputManager.GetComponent<PlayerSelectionScript>().playerCharIDs[2] = P3CharacterID;
                    playerInputManager.GetComponent<PlayerSelectionScript>().playerCharIDs[3] = P4CharacterID;

                    // Load the level
                    playerInputManager.GetComponent<PlayerSelectionScript>().Play();
                    
                    // This is set so that the level can't be loaded twice by accident
                    loadedNextLevel = true;
                }
            }
        }

    }

    // Just realised this isn't actually going to work
    //private void ChangeCharacterMat(GameObject characterObject, int materialID)
    //{
    //    //// Changes the colour of our clothing according to which player we are
    //    //GameObject clothingPiece = playerInst.GetComponent<PlayerControllerOldInput>().clothingPiece;
    //    //playerChildMats = clothingPiece.GetComponent<Renderer>().materials;
    //    //playerChildMats[0] = PlayerMaterials[playerID];
    //    //clothingPiece.GetComponent<Renderer>().materials = playerChildMats;

    //    GameObject clothingPiece = characterObject.GetComponent<PlayerControllerOldInput>().clothingPiece;
    //    Material[] clothingMats = clothingPiece.GetComponent<Renderer>().materials;
    //    clothingMats[0] = playerMats[materialID];
    //    clothingPiece.GetComponent<Renderer>().materials = clothingMats;
    //}
}
