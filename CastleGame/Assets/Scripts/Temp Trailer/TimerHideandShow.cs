using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerHideandShow : MonoBehaviour
{
    public Renderer rend;
    //public object objectToShow;
    public float time;

    // Start is called before the first frame update
    void Start()
    {

        Invoke("BooleanRender", time);

    }

    // Update is called once per frame
    public void BooleanRender()
    {

        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }
}
