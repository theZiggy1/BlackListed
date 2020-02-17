﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject battleArea;
    string PLAYER_TAG = "Player";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == PLAYER_TAG)
        {
            battleArea.GetComponent<EnemySpawnerScript>().playersHaveEntered();
            this.gameObject.SetActive(false);
        }
    }
}
