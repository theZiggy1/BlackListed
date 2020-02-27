using System.Collections;
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
    //When you enter the trigger area, this lets the area know so, and lets all players enter into the area. It also removes the trigger, so it cant be triggered a second time. 
    private void OnTriggerEnter(Collider other)
    {
        if(!enabled) { return; }
        if(other.gameObject.tag == PLAYER_TAG)
        {
            Debug.Log("Players entered");
            if (battleArea != null)
            {
                battleArea.GetComponent<EnemySpawnerScript>().playersHaveEntered();
            }
            this.gameObject.GetComponent<TriggerArea>().enabled = false;
        }
    }
}
