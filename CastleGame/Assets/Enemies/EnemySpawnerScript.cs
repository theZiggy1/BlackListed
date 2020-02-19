using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject wallBehind;
    [SerializeField] GameObject wallInfront;
    [SerializeField] GameObject Enemies;
    [SerializeField] Transform[] SpawnPoint;

    [SerializeField]  int numEnemies = 1;
    [SerializeField] Camera inGameCamera;
    [SerializeField] Transform camaraLocation;
    bool LerpTowards;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public  void playersHaveEntered()
    {
        wallBehind.SetActive(true);
        wallInfront.SetActive(true);
        GameObject Enemy = GameObject.Instantiate(Enemies, SpawnPoint[0].position, SpawnPoint[0].rotation);
        Enemy.GetComponent<EnemyScript>().Spawner = this.gameObject;
    }

    public void EnemyKilled()
    {
        numEnemies--;
        if(numEnemies == 0)
        {
            wallInfront.SetActive(false);
        }
    }
}
