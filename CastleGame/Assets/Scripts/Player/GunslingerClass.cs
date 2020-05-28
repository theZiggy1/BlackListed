using FMOD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/******************
 * Anton Ziegler s1907905
 * ****************/
public class GunslingerClass : BaseClass
{
    public float intialBulletVelocity;
    public float intialDodgeVelocity;
    public float jumpForce;
    public float timeUntilAnimFinished;
    public string ENEMY_TAG = "Enemy";
    [SerializeField] PlayerControllerOldInput inputScript;
    public float enemyDamage = 100;
    [SerializeField] Rigidbody gunslingerBody;
    [SerializeField] GameObject Brian;
    [SerializeField] GameObject ParticleEffect;
    
    [SerializeField] float timeToTurnOff = 1.0f;

    public string DUMMY_TAG = "dummyEnemy";
    public override void abilityAttack()
    {
        if (abilityCoolDown <= 0)
        {
            inputScript.DodgeAnim();
            abilityCoolDown = genericAttackReset;
            Vector3 forceVector = -Brian.transform.forward * intialDodgeVelocity;
            forceVector += new Vector3(0.0f, jumpForce, 0.0f);
            gunslingerBody.AddForce(forceVector);
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
            GameObject bullet = GameObject.Instantiate(basicAttackObj, basicAttackLocation.position, basicAttackLocation.rotation);
            GameObject effect = GameObject.Instantiate(ParticleEffect, basicAttackLocation.position, basicAttackLocation.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(basicAttackLocation.forward * intialBulletVelocity);
            Destroy(bullet, 0.5f);
            Destroy(effect, 0.5f);
        }
    }

    public override void ultraAttack()
    {
        if (ultraCoolDown <= 0)
        {
            ultraCoolDown = ultraCoolDownReset;


            GameObject[] enemies = GameObject.FindGameObjectsWithTag(ENEMY_TAG);
            GameObject[] targetDummies = GameObject.FindGameObjectsWithTag(DUMMY_TAG);
            GameObject Bullet = GameObject.Instantiate(ultraAttackObj, ultraLocation.position, ultraLocation.rotation);
            Destroy(Bullet, 0.5f);
            foreach ( GameObject EnemyObj in enemies)
            {
                EnemyObj.GetComponent<EntityScript>().TakeDamage(enemyDamage);
                //
            }

            foreach (GameObject dummyObj in targetDummies)
            {
                if (dummyObj != null)
                {
                    dummyObj.GetComponent<TargetDummyScript>().HitDummy();
                }
            }
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
        this.GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(WaitUntilTuronKin(0.001f));
    }

    IEnumerator WaitUntilTuronKin(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        this.GetComponent<Rigidbody>().isKinematic = false;
    }
}
