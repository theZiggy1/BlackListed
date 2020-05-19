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
    [Header("Player")]
    [SerializeField]
    [Tooltip("Is this entity a player?")]
    private bool isPlayer;

    [Header("Health")]
    [SerializeField]
    [Tooltip("The health we start with")]
    private float startingHealth;
    [SerializeField]
    [Tooltip("Our current health")]
    private float currentHealth;

    private float healthConverted; // This is our health converted so it's a value between 0 and 1

    [Header("Health UI")]
    [SerializeField]
    [Tooltip("Is our entity going to have a health bar?")]
    private bool usingHealthBar;

    [SerializeField]
    private Slider healthBar;

    [Header("Death")]
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

    [Header("Invulnerability")]
    [SerializeField]
    [Tooltip("This bool is dsoes not let players/enemies take damage while is active")] //Added by Anton, has a gettter and setter added, id have made this public, but i kep Farrans writing here. 
    private bool isInvulnerable = false;

    [Header("Audio")]
    [SerializeField]
    private bool usingAudio;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip audioClipHurt;
    [SerializeField]
    private AudioClip audioClipHealed;
    [SerializeField]
    private AudioClip audioClipHealingRift;
    [SerializeField]
    private AudioClip audioClipRevived;
    [SerializeField]
    private AudioClip audioClipDied;

    [Header("Loot")]
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

    [Space(10)]

    [SerializeField]
    [Tooltip("Do we emit particles when we die?")]
    private bool doesParticlesOnDeath;
    [SerializeField]
    [Tooltip("The particle object we drop on death")]
    private GameObject particleObject;

    [Space(10)]

    public bool isDead; // Used to say if we're dead

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

            if (GetComponent<BoxCollider>() == null)
            {
                Debug.Log("Box collider trigger not set on player! Therefore players won't be able to revive eachother!");
            }
        }

        currentHealth = startingHealth;

        if (usingHealthBar)
        {
            UpdateHealthBar();
        }

        if (usingAudio)
        {
            // For now, the audio source is on the same object as us, so this is fine
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // GET RID OF THIS AT SOME POINT
        //if (usingHealthBar)
        //{
        //    UpdateHealthBar();
        //}

        //if (currentHealth <= 0)
        //{
        //    Die();
        //}
    }

    private void UpdateHealthBar()
    {
        // !!! This needs to be done fractionally so that any health can correspond to a float between 0 and 1

        healthConverted = currentHealth / startingHealth;

        healthBar.value = healthConverted;
    }

    public void TakeDamage(float damage)
    {
        if (isInvulnerable)
        {
            return; //If we are invulnerable we dont need to calculate damage do we. 
        }

        // We only need to do this stuff if we're not already dead
        if (!isDead)
        {
            currentHealth -= damage;

            // Means that we don't get more health than our max
            if (currentHealth > startingHealth)
            {
                currentHealth = startingHealth;
            }

            if (usingAudio)
            {
                // Sets us to have the 'hurt' audio clip
                audioSource.clip = audioClipHurt;
                // Then plays that clip
                audioSource.Play();
            }


            if (usingHealthBar)
            {
                UpdateHealthBar();
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    // Done as an override so that other things don't break
    public void TakeDamage(float damage, bool isHealthPotion)
    {
        if(isInvulnerable)
        {
            return; //If we are invulnerable we dont need to calculate damage do we. 
        }

        // We only need to do this stuff if we're not already dead
        if (!isDead)
        {
            // If we're a health potion, heal us
            if (isHealthPotion)
            {
                currentHealth += damage;

                // Means that we don't get more health than our max
                if (currentHealth > startingHealth)
                {
                    currentHealth = startingHealth;
                }

                if (usingAudio)
                {
                    // Sets us to have the 'healed' audio clip
                    audioSource.clip = audioClipHealed;
                    // Then plays that clip
                    audioSource.Play();
                }
            }
            else // Otherwise, do damage to us
            {
                currentHealth -= damage;

                // Means that we don't get more health than our max
                if (currentHealth > startingHealth)
                {
                    currentHealth = startingHealth;
                }

                if (usingAudio)
                {
                    // Sets us to have the 'hurt' audio clip
                    audioSource.clip = audioClipHurt;
                    // Then plays that clip
                    audioSource.Play();
                }
            }

            if (usingHealthBar)
            {
                UpdateHealthBar();
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void HealingRift(float healing)
    {
        if (isInvulnerable)
        {
            return; //If we are invulnerable we dont need to calculate damage do we. 
        }

        // We only need to do this stuff if we're not already dead
        if (!isDead)
        {
            currentHealth += healing;

            // Means that we don't get more health than our max
            if (currentHealth > startingHealth)
            {
                currentHealth = startingHealth;
            }

            if (usingAudio)
            {
                // Sets us to have the 'healing rift' audio clip
                audioSource.clip = audioClipHealingRift;
                // Then plays that clip
                audioSource.Play();
            }

            if (usingHealthBar)
            {
                UpdateHealthBar();
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    // This script can be called by other things, to revive us, if we're dead
    public void Revive()
    {
        if (isDead)
        {
            currentHealth = startingHealth;

            if (usingHealthBar)
            {
                UpdateHealthBar();
            }
            

            if (isPlayer)
            {
                //!!!Probably also put a thing here to tell the player that their controls are unlocked!!!
                // E.g.
                // GetComponent<PlayerControllerOldInput>().Revive();

                GetComponent<PlayerControllerOldInput>().Revive();

                //// Disables the box collider trigger, so that other players can't trigger us anymore, as we're now alive
                //GetComponent<BoxCollider>().enabled = false;
            }

            if (usingAudio)
            {
                // Sets us to have the 'revived' audio clip
                audioSource.clip = audioClipRevived;
                // Then plays that clip
                audioSource.Play();
            }

            isDead = false;
        }
    }

    private void Die()
    {
        isDead = true;

        if (isPlayer)
        {
            //!!!Probably also put a thing here to tell the player that their controls are locked!!!
            // E.g.
            // GetComponent<PlayerControllerOldInput>().Die();
            // All this should do on the player's side, is lock their controls
            // Then we'll play a death animation below here

            GetComponent<PlayerControllerOldInput>().Die();

            //// Enables the box collider trigger, so that other players can trigger us
            //GetComponent<BoxCollider>().enabled = true;
        }


        // This 'plays' but you don't actually have enough time to hear it,
        // as the player's gameobject is destroyed before you can hear it
        if (usingAudio)
        {
            // Sets us to have the 'died' audio clip
            audioSource.clip = audioClipDied;
            // Then plays that clip
            audioSource.Play();
        }

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

        if (doesParticlesOnDeath)
        {
            Instantiate(particleObject, transform.position, transform.rotation);
        }

        // Will only happen for enemies
        if (!isPlayer)
        {
            // This is temporary
            Debug.Log(this.gameObject.name + " has died");
            Destroy(this.gameObject);
        }
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
            Instantiate(itemToDrop, transform.position, itemToDrop.transform.rotation);
        }
        else
        {
            Debug.Log("No item dropped");
        }

    }

    // Used for the revive mechanic
    private void OnTriggerStay(Collider other)
    {
        // If we're dead
        if (isDead)
        {
            // If we're a player
            if (other.gameObject.CompareTag("Player"))
            {
                // If the other player presses the B button
                if (Input.GetButtonDown("Joy" + other.gameObject.GetComponent<PlayerControllerOldInput>().playerID + "ButtonB"))
                {
                    Revive();
                }

            }
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
