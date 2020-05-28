using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************
 * Anton Ziegler s1907905
 * ****************/
public class BellHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public int TotalNumBells = 0;
    int currentRungBells = 0;
    public void RingBell()
    {
        currentRungBells++;
        Debug.Log("Rang but not done");
        if(currentRungBells == TotalNumBells)
        {

            Debug.Log("Rang all the bells!");
        }
    }

    public void AddBell()
    {
        TotalNumBells++;
    }
}
