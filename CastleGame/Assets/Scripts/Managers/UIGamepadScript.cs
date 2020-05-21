using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGamepadScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Put any buttons in the UI here, in order of how they'll be selected, e.g. top to bottom")]
    private GameObject[] buttonArray;

    [SerializeField]
    private GameObject buttonHighlight; // Changed at runtime to be the highlight of the currently selected button

    [SerializeField]
    [Tooltip("How we're going to navigate the UI with the controller")]
    private NavigationType navigationType;

    private enum NavigationType
    {
        topToBottom,
        bottomToTop,
        leftToRight,
        rightToLeft,
        numNavigationTypes
    }

    //[SerializeField]
    //[Tooltip("Do we transition to another menu in this scene?")]
    //private bool transitionsToOtherMenu;

    //[SerializeField]
    //[Tooltip("The other UIGamepadManager we transition to")]
    //private GameObject otherUIGamepadManager;

    [SerializeField]
    private int currentButtonIndex; // The index in the array of the current button we have selected

    [SerializeField]
    private bool movedButtons; // Used to say if we've moved buttons, so that we can't hold down and keep moving

    // Start is called before the first frame update
    void Start()
    {
        if (navigationType == NavigationType.bottomToTop)
        {
            // Sets our selected button to be the bottom one
            currentButtonIndex = buttonArray.Length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!movedButtons)
        {
            // We've moved the stick down
            if (Input.GetAxis("Joy1LeftStickVertical") < -0.5f)
            {
                Debug.Log("Moving down");

                // Sets our current buttonHighlight to inactive once we move off it
                buttonHighlight.SetActive(false);

                currentButtonIndex++;

                // If the index gets larger
                if (currentButtonIndex >= buttonArray.Length)
                {
                    // Then we wrap it back around to 0
                    currentButtonIndex = 0;
                }
                //// If the index gets smaller than 0
                //if (currentButtonIndex < 0)
                //{
                //    // Then we wrap it back around to the length of the array
                //    currentButtonIndex = buttonArray.Length - 1;
                //}

                // Look for the highlight object
                foreach (Transform child in buttonArray[currentButtonIndex].transform)
                {
                    if (child.CompareTag("ButtonHighlight"))
                    {
                        // Once we've found it, break out of loop
                        buttonHighlight = child.gameObject;
                        break;
                    }
                }

                // Sets the new buttonHighlight to active once we're on it
                buttonHighlight.SetActive(true);


                // Setting this so we can't just hold down the stick and keep moving
                movedButtons = true;
            }

            // We've moved the stick up
            if (Input.GetAxis("Joy1LeftStickVertical") > 0.5f)
            {
                Debug.Log("Moving up");

                // Sets our current buttonHighlight to inactive once we move off it
                buttonHighlight.SetActive(false);

                currentButtonIndex--;

                //// If the index gets larger
                //if (currentButtonIndex >= buttonArray.Length)
                //{
                //    // Then we wrap it back around to 0
                //    currentButtonIndex = 0;
                //}
                // If the index gets smaller than 0
                if (currentButtonIndex < 0)
                {
                    // Then we wrap it back around to the length of the array
                    currentButtonIndex = buttonArray.Length - 1;
                }

                // Look for the highlight object
                foreach (Transform child in buttonArray[currentButtonIndex].transform)
                {
                    if (child.CompareTag("ButtonHighlight"))
                    {
                        // Once we've found it, break out of loop
                        buttonHighlight = child.gameObject;
                        break;
                    }
                }

                // Sets the new buttonHighlight to active once we're on it
                buttonHighlight.SetActive(true);


                // Setting this so we can't just hold down the stick and keep moving
                movedButtons = true;
            }

            if (Input.GetAxis("Joy1LeftStickHorizontal") > 0.5f)
            {
                Debug.Log("Moving left");

                // If it's a slider we're moving
                if (buttonArray[currentButtonIndex].GetComponent<Slider>() != null)
                {
                    buttonArray[currentButtonIndex].GetComponent<Slider>().value -= 0.1f;
                }

                // Setting this so we can't just hold down the stick and keep moving
                movedButtons = true;
            }
            if (Input.GetAxis("Joy1LeftStickHorizontal") < -0.5f)
            {
                Debug.Log("Moving right");

                // If it's a slider we're moving
                if (buttonArray[currentButtonIndex].GetComponent<Slider>() != null)
                {
                    buttonArray[currentButtonIndex].GetComponent<Slider>().value += 0.1f;
                }

                // Setting this so we can't just hold down the stick and keep moving
                movedButtons = true;
            }

        }

        // If our stick is back in the centre, then
        if ((Input.GetAxis("Joy1LeftStickVertical") < 0.25f && Input.GetAxis("Joy1LeftStickVertical") > -0.25f) &&
            (Input.GetAxis("Joy1LeftStickHorizontal") < 0.25f && Input.GetAxis("Joy1LeftStickHorizontal") > -0.25f))
        {
            // Reset this so we can move images again
            movedButtons = false;
        }
        //// If our stick is back in the centre, then
        //if (Input.GetAxis("Joy1LeftStickHorizontal") < 0.25f && Input.GetAxis("Joy1LeftStickHorizontal") > -0.25f)
        //{
        //    // Reset this so we can move images again
        //    movedButtons = false;
        //}



        if (Input.GetButtonDown("Joy1ButtonA"))
        {
            // If it's a button we're clicking
            if (buttonArray[currentButtonIndex].GetComponent<Button>() != null)
            {
                // 'Clicks' the button that is at the current position of our array
                buttonArray[currentButtonIndex].GetComponent<Button>().onClick.Invoke();
            }
            // If it's a slider we're moving
            if (buttonArray[currentButtonIndex].GetComponent<Slider>() != null)
            {
                buttonArray[currentButtonIndex].GetComponent<Slider>().value += 0.1f;
            }
        }
        if (Input.GetButtonDown("Joy1ButtonB"))
        {
            // If it's a slider we're moving
            if (buttonArray[currentButtonIndex].GetComponent<Slider>() != null)
            {
                buttonArray[currentButtonIndex].GetComponent<Slider>().value -= 0.1f;
            }
        }
        


    }

}
