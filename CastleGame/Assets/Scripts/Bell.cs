using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour
{
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
