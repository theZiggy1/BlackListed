using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Farran Holmes s1712383
/// </summary>
public class SpriteBillboardScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Putting in LateUpdate so that it avoids any potential flickering
    private void LateUpdate()
    {
        // Sets our forward to be the same as the main camera,
        // that way we always look in the same direction as it
        transform.forward = Camera.main.transform.forward;
    }
}
