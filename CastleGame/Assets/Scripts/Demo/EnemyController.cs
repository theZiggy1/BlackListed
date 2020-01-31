using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy script
/// By Farran Holmes
/// Created on 31/01/2020
/// </summary>

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float startingHealth; // The starting health of the enemy
    [SerializeField]
    private float currentHealth; // The current health of the enemy

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This can be called by a player's weapon when we get attacked
    public void DamageSelf(float damage)
    {
        currentHealth -= damage;

        // If our health reaches 0, then we die
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // This gets called when we die
    private void Die()
    {

    }
}
