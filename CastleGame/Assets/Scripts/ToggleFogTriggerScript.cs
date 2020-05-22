using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFogTriggerScript : MonoBehaviour
{
    [SerializeField]
    private bool toggleFogOnTriggerEnter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Turn fog on when we collide
            if (toggleFogOnTriggerEnter)
            {
                RenderSettings.fog = true;
            }
            else // Turn fog off when we collide
            {
                RenderSettings.fog = false;
            }
        }
    }
}
