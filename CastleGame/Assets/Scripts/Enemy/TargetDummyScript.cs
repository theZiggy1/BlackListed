using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************
 * Anton Ziegler s1907905
 * ****************/
public class TargetDummyScript : MonoBehaviour
{
    //This is for the test dummies in the tavern of the first level. when they are hit they play an amimation. 
    // Start is called before the first frame update
    [SerializeField] Animator dummyAnimator;
    [SerializeField] float timeToTurnOff = 1.0f;

  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitDummy()
    {

        dummyAnimator.SetBool("dummyHit", true);
        StartCoroutine(dummyStopHit(timeToTurnOff));
    }

    IEnumerator dummyStopHit(float timeToWait)
    {
    
        yield return new WaitForSeconds(timeToWait);
        dummyAnimator.SetBool("dummyHit", false);
    }
}
