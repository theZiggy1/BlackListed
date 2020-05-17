using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerClass : BaseClass
{
    public float forceStrength;
    public float rangerUltraVel;
    public float[] ArrayDelay;
    public override void abilityAttack()
    {
        if (abilityCoolDown <= 0)
        {
            abilityCoolDown = abilityCoolDownReset;
            GameObject Arrow = GameObject.Instantiate(abilityAttackObj, abilityLocation.position, abilityLocation.rotation);
            Arrow.GetComponent<Rigidbody>().AddForce(abilityLocation.forward * forceStrength);
            Destroy(Arrow, 5.0f);
            StartCoroutine("DelayedArrow", ArrayDelay[0]);
            StartCoroutine("DelayedArrow", ArrayDelay[1]);
        }
    }
    public override void genericAttack()
    {
        if (genericAattackCoolDown <= 0)
        {
            genericAattackCoolDown = genericAttackReset;
            GameObject Bullet = GameObject.Instantiate(basicAttackObj, basicAttackLocation.position, basicAttackLocation.rotation);
            Bullet.GetComponent<Rigidbody>().AddForce(basicAttackLocation.forward * forceStrength);
            Destroy(Bullet, 5.0f);
        }
    }

    public override void ultraAttack()
    {
        if (ultraCoolDown <= 0)
        {
            ultraCoolDown = ultraCoolDownReset;

            GameObject Arrow = GameObject.Instantiate(ultraAttackObj, ultraLocation.position, ultraLocation.rotation);
            Arrow.GetComponent<Rigidbody>().AddForce(ultraLocation.forward * rangerUltraVel);
            Destroy(Arrow, 10.0f);
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

    IEnumerator DelayedArrow(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        GameObject Arrow = GameObject.Instantiate(abilityAttackObj, abilityLocation.position, abilityLocation.rotation);
        Arrow.GetComponent<Rigidbody>().AddForce(basicAttackLocation.forward * forceStrength);
        Destroy(Arrow, 5.0f);
    }

}
