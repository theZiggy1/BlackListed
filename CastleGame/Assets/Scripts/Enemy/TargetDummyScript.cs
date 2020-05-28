using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummyScript : MonoBehaviour
{
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
