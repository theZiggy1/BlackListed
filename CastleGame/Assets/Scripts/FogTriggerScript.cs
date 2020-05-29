using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Farran Holmes s1712383
/// </summary>

public class FogTriggerScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Do we turn the fog on when we hit this trigger, or turn it off?")]
    private bool fogOn;
    [SerializeField]
    private float desiredFogDensity = 0.02f;
    [SerializeField]
    private float currentFogDensity;
    [SerializeField]
    private float fogIncrement = 0.001f;
    [SerializeField]
    private float fadeInterval = 0.1f;

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
        if (other.CompareTag("Player"))
        {
            currentFogDensity = RenderSettings.fogDensity;
            StartCoroutine(FadeFog());
        }
    }

    private IEnumerator FadeFog()
    {
        if (fogOn)
        {
            RenderSettings.fog = true;

            while (currentFogDensity < desiredFogDensity)
            {
                currentFogDensity += fogIncrement;
                RenderSettings.fogDensity = currentFogDensity;

                yield return new WaitForSeconds(fadeInterval);
            }
        }
        if (!fogOn)
        {
            while (currentFogDensity > 0)
            {
                currentFogDensity -= fogIncrement;
                RenderSettings.fogDensity = currentFogDensity;

                yield return new WaitForSeconds(fadeInterval);
            }

            RenderSettings.fog = false;
        }
    }
}
