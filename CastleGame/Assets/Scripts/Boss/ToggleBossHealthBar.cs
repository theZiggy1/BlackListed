using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Farran Holmes s1712383
/// </summary>
public class ToggleBossHealthBar : MonoBehaviour
{
    [SerializeField]
    private GameObject healthBarObject;

    // Start is called before the first frame update
    void Start()
    {
        DisableHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableHealthBar()
    {
        healthBarObject.SetActive(true);
    }
    public void DisableHealthBar()
    {
        healthBarObject.SetActive(false);
    }
}
