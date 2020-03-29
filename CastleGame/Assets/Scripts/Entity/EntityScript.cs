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

    [SerializeField]
    [Tooltip("This bool is dsoes not let players/enemies take damage while is active")] //Added by Anton, has a gettter and setter added, id have made this public, but i kep Farrans writing here. 
    private bool isInvulnerable = false;
    private enum DeathType
    {
        Animation,
        NoTransition,
        ParticleEffect,
        AnimAndParticles,
        NumStates
    }

    [SerializeField]
    [Tooltip("Does the entity have a chance to drop something on death?")]
    private bool canDropItem;

    [SerializeField]
    [Tooltip("Array of anything the entity can potentially drop")]
    private GameObject[] itemDrops;

    [SerializeField]
    [Tooltip("The drop chances (in %), corresponding to the same points in the itemDrops array ")]
    private float[] dropChances;

    [SerializeField]
    private float randomNumber; // The random number that we generate, for use with the loot system

    [SerializeField]
    private GameObject itemToDrop; // The item that is going to get dropped

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
        if(isInvulnerable)
        {
            return; //If we are invulnerable we dont need to calculate damage do we. 
        }

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

        if (canDropItem)
        {
            DropItem();
        }

        // This is temporary
        Debug.Log(this.gameObject.name + " has died");
        Destroy(this.gameObject);
    }

    private void DropItem()
    {
        // Gives us a random number (percentage)
        randomNumber = Random.Range(0f, 100f);

        for (int i = 0; i <= itemDrops.Length; i++)
        {
            // If the random number is less than the chance, we drop an item
            if (randomNumber <= dropChances[i])
            {
                // Stores the item we drop
                itemToDrop = itemDrops[i];

                // Then breaks out of the for loop
                break;
            }
            else
            {
                // Subtract the current drop chance, so we can check again
                randomNumber -= dropChances[i];
            }
        }

        if (itemToDrop != null)
        {
            Debug.Log("Dropped an item");
            // Spawns the item where the entity currently is
            Instantiate(itemToDrop, transform.position, transform.rotation);
        }
        else
        {
            Debug.Log("No item dropped");
        }

    }

    //These two have been aded by Anton, along with the isInvulnerable var in this way to keep with how Farran wrote the script.
    //Id have made it public so my other scripts could handle the var directly, but what do i know. 
    public bool GetInvulnerable()
    {
        return isInvulnerable;
    }

    public void SetInvulnerable(bool setInvulnerableState)
    {
        isInvulnerable = setInvulnerableState;
    }

    //Added this so other scripts can at least access the info of what the current Health is.
    public float GetCurHealth()
    {
        return currentHealth;
    }
} 
