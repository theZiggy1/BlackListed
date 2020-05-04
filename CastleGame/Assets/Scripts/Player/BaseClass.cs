using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseClass : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform basicAttackLocation;
    [SerializeField] Transform abilityLocation;
    [SerializeField] Transform ultraLocation;

    [SerializeField] GameObject basicAttackObj;
    [SerializeField] GameObject abilityAttackObj;
    [SerializeField] GameObject ultraAttackObj;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void genericAttack();

    public abstract void abilityAttack();

    public abstract void ultraAttack();
    

    

    

    


}
