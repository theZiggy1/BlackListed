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
    [SerializeField] Transform[] movePlayers;
    [SerializeField] GameObject gameManager;
    [SerializeField]  int numEnemies = 2;
    [SerializeField] Camera inGameCamera;
    [SerializeField] Transform cameraLocation;
    bool LerpTowards = false;
    public float speed;

    private string GAMEMANAGER_TAG = "GameManager";
    void Start()
    {

        gameManager = GameObject.FindGameObjectWithTag(GAMEMANAGER_TAG);
    }

    // Update is called once per frame
    void Update()
    {
        if(LerpTowards)
        {
            inGameCamera.transform.position = Vector3.MoveTowards(inGameCamera.transform.position, cameraLocation.position, speed * Time.deltaTime);
            if(Vector3.Distance(inGameCamera.transform.position, cameraLocation.position) < 0.01f)
            {
                LerpTowards = false;
            }
        }
    }

   public  void playersHaveEntered()
    {
        wallBehind.SetActive(true);
        wallInfront.SetActive(true);
        GameObject Enemy = GameObject.Instantiate(Enemies, SpawnPoint[0].position, SpawnPoint[0].rotation);
        Enemy.GetComponent<EnemyScript>().Spawner = this.gameObject;
        GameObject Enemy2 = GameObject.Instantiate(Enemies, new Vector3(SpawnPoint[0].position.x + 5, SpawnPoint[0].position.y, SpawnPoint[0].position.z), SpawnPoint[0].rotation);
        Enemy2.GetComponent<EnemyScript>().Spawner = this.gameObject;


        foreach(GameObject players in gameManager.GetComponent<GameManagerScript>().currentPlayers)
        {

            if (players != null)
            {
                players.transform.position = movePlayers[players.GetComponent<PlayerController>().playerNum].transform.position;
            }
        }
        LerpTowards = true;
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
