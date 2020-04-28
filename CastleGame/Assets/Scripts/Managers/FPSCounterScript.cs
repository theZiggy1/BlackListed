using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounterScript : MonoBehaviour
{
    [SerializeField]
    private float currentFPS;
    [SerializeField]
    private float currentFPSInt;

    [SerializeField]
    private Text FPSDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //currentFPS = 0;
        currentFPS = 1f / Time.unscaledDeltaTime;
        currentFPSInt = (int)currentFPS;
        FPSDisplay.text = currentFPSInt.ToString();
    }
}
