using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDamageScript : MonoBehaviour
{
    // This script is meant to be attached to anything that can damage an entity
    // e.g. A weapon, a bullet, a stalactite

    [SerializeField]
    [Tooltip("The amount of damage this object will do when it hits an entity, or the amount it will heal a player, !Remember! This object should have a trigger on it!")]
    private float damageOrHealing;

    [SerializeField]
    [Tooltip("If we're a health potion, we'll only 'damage' players and not enemies")]
    private bool isHealthPotion;

    [SerializeField]
    [Tooltip("If we're a healing rift, we'll only 'damage' players and not enemies")]
    private bool isHealingRift;

    [SerializeField]
    [Tooltip("If an entity interacts with us, should we destroy ouselves once they do?")]
    private bool destroyOnTouch;

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
        if (isHealthPotion)
        {
            // If we hit anything tagged with player, or enemy
            if (other.CompareTag("Player"))
            {
                // If it has an entity script on it
                if (other.GetComponent<EntityScript>() != null)
                {
                    other.GetComponent<EntityScript>().TakeDamage(damageOrHealing, isHealthPotion);

                    if (destroyOnTouch)
                    {
                        DestroySelf();
                    }
                }
                else
                {
                    // Outputs this log, as players and enemies should have entity scripts
                    Debug.Log("We've hit a player (as a health potion) but they don't have an entity script!");
                }
            }
        }
        else if (isHealingRift)
        {
            // If we hit anything tagged with player, or enemy
            if (other.CompareTag("Player"))
            {
                // If it has an entity script on it
                if (other.GetComponent<EntityScript>() != null)
                {
                    // I know this isn't best practice but it works
                    other.GetComponent<EntityScript>().HealingRift(damageOrHealing);

                    if (destroyOnTouch)
                    {
                        DestroySelf();
                    }
                }
                else
                {
                    // Outputs this log, as players and enemies should have entity scripts
                    Debug.Log("We've hit a player (as a healing rift) but they don't have an entity script!");
                }
            }
        }
        else // If we're not a health potion
        {
            // If we hit anything tagged with player, or enemy
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                // If it has an entity script on it
                if (other.GetComponent<EntityScript>() != null)
                {
                    other.GetComponent<EntityScript>().TakeDamage(damageOrHealing);

                    if (destroyOnTouch)
                    {
                        DestroySelf();
                    }
                }
                else
                {
                    // Outputs this log, as players and enemies should have entity scripts
                    Debug.Log("We've hit a player/enemy but they don't have an entity script!");
                }
            }
        }
    }

    // Called if we're told to destroy ourselves once we hit an entity
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
