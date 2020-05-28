using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************
 * Anton Ziegler s1907905
 * ****************/
public class Bell : MonoBehaviour
{
    //This handled updating an ununsed bell mechanic, that wouls allow you to progress through a level. 
    //This called to a central object when "rung", and when all were rang, it wouild spawn a new room to fight a miniboss. 
    GameObject BellHandler;
    public string BELLHAND_TAG = "BellHandler";
    // Start is called before the first frame update
    void Start()
    {
        BellHandler = GameObject.FindGameObjectWithTag(BELLHAND_TAG);
        BellHandler.GetComponent<BellHandler>().AddBell();
    }

    // Update is called once per frame


    public void RingingBell()
    {
        if(this.enabled == false)
        {
            return;
        }
        Debug.Log("Rang");
        this.enabled = false;
        BellHandler.GetComponent<BellHandler>().RingBell();
    }
}
