using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Farran Holmes
/// s1712383
/// </summary>

public class EntityScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Is this entity a player?")]
    private bool isPlayer;

    [SerializeField]
    [Tooltip("The health we start with")]
    private float startingHealth;
    [SerializeField]
    [Tooltip("Our current health")]
    private float currentHealth;

    private float healthConverted; // This is our health converted so it's a value between 0 and 1

    [SerializeField]
    [Tooltip("Is our entity going to have a health bar?")]
    private bool usingHealthBar;

    [SerializeField]
    private Slider healthBar;

    [SerializeField]
    [Tooltip("The type of transition that happens when the entity dies")]
    private DeathType deathType;

    private enum DeathType
    {
        Animation,
        NoTransition,
        ParticleEffect,
        AnimAndParticles,
        NumStates
    }

    // Start is called before the first frame update
    void Start()
    {
        // Are we a player?
        if (isPlayer)
        {
            // Then we need to find the relevant UI for the player
            int playerID;
            GameObject playerUI;

            // This will *only* work if we are a player and this is on the same game object as the player script
            playerID = GetComponent<PlayerControllerOldInput>().playerID;

            // Gets the UI parent object with the relevant tag
            playerUI = GameObject.FindGameObjectWithTag("Player" + playerID + "UI");

            if (playerUI == null)
            {
                Debug.Log("PlayerUI not set up!");
            }
            else
            {
                foreach (Transform childObject in playerUI.transform)
                {
                    // Finds the child object tagged as HealthBar
                    if (childObject.CompareTag("HealthBar"))
                    {
                        // Gets the slider from the child game object
                        healthBar = childObject.gameObject.GetComponent<Slider>();
                    }
                }

            }
        }

        currentHealth = startingHealth;

        if (usingHealthBar)
        {
            UpdateHealthBar();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // GET RID OF THIS AT SOME POINT
        if (usingHealthBar)
        {
            UpdateHealthBar();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        // !!! This needs to be done fractionally so that any health can correspond to a float between 0 and 1

        healthConverted = currentHealth / startingHealth;

        healthBar.value = healthConverted;
    }

    // Called by other things when they damage us
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (usingHealthBar)
        {
            UpdateHealthBar();
        }        
    }

    private void Die()
    {
        // These need to be done

        if (deathType == DeathType.Animation)
        {

        }
        if (deathType == DeathType.NoTransition)
        {

        }
        if (deathType == DeathType.ParticleEffect)
        {

        }
        if (deathType == DeathType.AnimAndParticles)
        {

        }

        // This is temporary
        Debug.Log(this.gameObject.name + " has died");
        Destroy(this.gameObject);
    }
}
