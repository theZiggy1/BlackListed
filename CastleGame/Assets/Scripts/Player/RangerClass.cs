using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************
 * Anton Ziegler s1907905
 * ****************/
public class RangerClass : BaseClass
{

    //This is the classes for the ranger. in PLayer controller we call into these functions
    public float forceStrength;
    public float rangerUltraVel;
    public float[] ArrayDelay;
    //This spawns the base attack 3 times. 
    public override void abilityAttack()
    {
        if (abilityCoolDown <= 0)
        {
            abilityCoolDown = abilityCoolDownReset;
            GameObject Arrow = GameObject.Instantiate(abilityAttackObj, abilityLocation.position, abilityLocation.rotation);
            Arrow.GetComponent<Rigidbody>().AddForce(abilityLocation.forward * forceStrength);
            Destroy(Arrow, 3.0f);
            StartCoroutine("DelayedArrow", ArrayDelay[0]);
            StartCoroutine("DelayedArrow", ArrayDelay[1]);
        }
    }
    //This spawns one arrow
    public override void genericAttack()
    {
        if (genericAattackCoolDown <= 0)
        {
            genericAattackCoolDown = genericAttackReset;
            GameObject Bullet = GameObject.Instantiate(basicAttackObj, basicAttackLocation.position, basicAttackLocation.rotation);
            Bullet.GetComponent<Rigidbody>().AddForce(basicAttackLocation.forward * forceStrength);
            Destroy(Bullet, 3.0f);
        }
    }

    //This spawns the large arrow 
    public override void ultraAttack()
    {
        if (ultraCoolDown <= 0)
        {
            ultraCoolDown = ultraCoolDownReset;

            GameObject Arrow = GameObject.Instantiate(ultraAttackObj, ultraLocation.position, ultraLocation.rotation);
            Arrow.GetComponent<Rigidbody>().AddForce(ultraLocation.forward * rangerUltraVel);
            Destroy(Arrow, 3.0f);
        }
    }
// Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        abilityCoolDown -= Time.deltaTime;
        ultraCoolDown -= Time.deltaTime;
        genericAattackCoolDown -= Time.deltaTime;
    }
    //This is called to call 2 extra arrows for the ranger. 
    IEnumerator DelayedArrow(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        GameObject Arrow = GameObject.Instantiate(abilityAttackObj, abilityLocation.position, abilityLocation.rotation);
        Arrow.GetComponent<Rigidbody>().AddForce(basicAttackLocation.forward * forceStrength);
        Destroy(Arrow, 3.0f);
    }

}
