using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossAi : MonoBehaviour
{

    enum States
    {
        Phase1Slow,
        Phase2Fast,
        Phase3Enraged,
        Invulnerable,
        numStates
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack1GroundPound() //This attack creates 4 walls that the players must jump over.
    {

    }

    void Attack2ThrowRock() //This attack has the boss throw a rock at the current location of a randomly chosen player
    {

    }

    //The boss moves slowly in the first phase, but moves more quickly, and does this attack more in the second phase. 
    void Attack3Melee() //The boss moves quickly towards a current player, and then when close enough swipes at the player
    {

    }
    //The two multi attacks are for the third phase of the boss. 

    void Attack4MultiThrow() //The boss performs the Throw rock attack multiple times, between x-y times, at randomly chosen players
    {

    }

    void Attack5MultiSwipe() //The boss rushes a random player and swipes at them x-y times. 
    {

    }
}
