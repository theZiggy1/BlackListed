using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            //sliderValue = Mathf.Round(sliderValue * 10f) / 10f;
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
