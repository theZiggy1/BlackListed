using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Weapon script
/// By Farran Holmes
/// Created on 31/01/2020
/// </summary>

public class WeaponScript : MonoBehaviour
{
    // Weapon stats - public - so the player can read them
    [Tooltip("How much damage the weapon does when it hits something")]
    public float damage;
    [Tooltip("How fast we can attack with the weapon, in Hz (per second)")]
    public float attackRate; // How fast we can attack with the weapon, measured in Hz

    // Weapon stats - private - the ones the player doesn't need to know about
    [SerializeField]
    [Tooltip("Our starting weapons should be indestructible. If they have durability set this to false/unticked")]
    private bool isIndestructible; // Is the weapon indestructible? - Our starting weapons will be
    [SerializeField]
    [Tooltip("Starting durability of the weapon - think of this like HP for a weapon")]
    private float startDurability; // If the weapon isn't indestructible, how much durability does it normally have?
    [SerializeField]
    [Tooltip("The current durability of the weapon - each time we hit something this reduces by 1")]
    private float currentDurability; // The current durability of the weapon

    [SerializeField]
    private GameObject attackBox; // This is a trigger box that we enable during the frame(s) of animation we attack on

    private float attackRateConverted; // Attack rate converted to seconds (1 / Hz)


    // Start is called before the first frame update
    void Start()
    {
        currentDurability = startDurability;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This function will be called by the user of the weapon to use/attack with the weapon
    public void UseWeapon()
    {
        // Here we will play the attack animation, and then probably create an attack volume/trigger
        // for the frame of the animation where we actually make contact

        // For now I'm just going to have the trigger attack box show for a period of time, I'll tie it to animations later
        // For now I'll just have the box appear, then I'll put a timer on it appearing


        //!!! This code will be put on the player instead


        // After attacking we will reduce our durability by 1
        currentDurability--;

        if (currentDurability <= 0)
        {
            DestroyWeapon();
        }
    }

    // This function will be called when the weapon gets destroyed (durability = 0)
    private void DestroyWeapon()
    {
        // We will change this later on to work better
        Destroy(gameObject);
    }
}
