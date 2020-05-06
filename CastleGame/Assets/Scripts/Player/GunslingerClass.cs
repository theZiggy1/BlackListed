using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunslingerClass : BaseClass
{
    public float forceStrength;
    public float timeUntilAnimFinished;

    [SerializeField] PlayerControllerOldInput inputScript;
    public override void abilityAttack()
    {
        if (abilityCoolDown <= 0)
        {
            abilityCoolDown = genericAttackReset;
            this.gameObject.transform.position = abilityLocation.position;
            genericAattackCoolDown = 0.0f;

            inputScript.isTumbling = true;
            StartCoroutine("WaitUntilTumble", timeUntilAnimFinished);
        }
    }

    public override void genericAttack()
    {
        if (genericAattackCoolDown <= 0)
        {
            genericAattackCoolDown = genericAttackReset;
            GameObject Bullet = GameObject.Instantiate(basicAttackObj, basicAttackLocation.position, basicAttackLocation.rotation);
            Bullet.GetComponent<Rigidbody>().AddForce(basicAttackLocation.forward * forceStrength);
            Destroy(Bullet, 0.5f);
        }
    }

    public override void ultraAttack()
    {
        if (ultraCoolDown <= 0)
        {
            ultraCoolDown = ultraCoolDownReset;
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

    IEnumerator WaitUntilTumble(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        inputScript.isTumbling = false;
    }
}
