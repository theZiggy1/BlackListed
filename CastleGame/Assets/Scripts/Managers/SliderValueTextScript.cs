using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Farran Holmes s1712383
/// </summary>
public class SliderValueTextScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeText(System.Single sliderValue)
    {
        if (GetComponent<Text>() != null)
        {
            //// Rounds the number to the nearest 0.1
            // Sets to be between 0 and 100, and rounds the number
            sliderValue = Mathf.Round(sliderValue * 100f);
            // Sets text to have that value
            GetComponent<Text>().text = sliderValue.ToString();
        }
        else
        {
            Debug.Log("No text found on this object!");
        }
    }
}
