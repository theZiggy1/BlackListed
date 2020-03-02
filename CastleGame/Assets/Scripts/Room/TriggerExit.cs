using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExit : MonoBehaviour
{
    [SerializeField] GameObject battleArea;
    string PLAYER_TAG = "Player";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This calls a slightly different function from the other script. might be worth rewriting the single script with a bool, the housing two different scripts that almost do exactly the same thing. 


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PLAYER_TAG)
        {
            battleArea.GetComponent<EnemySpawnerScript>().playersHaveExited();
            this.gameObject.SetActive(false);
            
        }
    }
}
