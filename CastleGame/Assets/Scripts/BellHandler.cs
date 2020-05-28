using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************
 * Anton Ziegler s1907905
 * ****************/
public class BellHandler : MonoBehaviour
{
    // Start is called before the first frame update
    //This was created to handle a bell mechanic that didnt make it into the game. the goal was to "ring" the bells and load the next level using that, with a monoboss

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
