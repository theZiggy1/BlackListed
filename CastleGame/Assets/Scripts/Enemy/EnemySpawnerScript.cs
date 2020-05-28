using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/******************
 * Anton Ziegler s1907905
 * ****************/
public class EnemySpawnerScript : MonoBehaviour
{
    //This script predates the testspawnerscript, and also moved the camera to the room. this was prior to changes we had for the room idea. 
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

    public bool falling = false;
    public float FallSpeed = 5.0f;

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
            //inGameCamera.transform.position = Vector3.MoveTowards(inGameCamera.transform.position, cameraLocation.position, speed * Time.deltaTime);
            //if(Vector3.Distance(inGameCamera.transform.position, cameraLocation.position) < 0.01f)
            //{
            //    LerpTowards = false;
            //}

            inGameCamera.GetComponent<CameraMoveScript>().moveToRoom = true;
            inGameCamera.GetComponent<CameraMoveScript>().roomTargetObject = cameraLocation;
        }

        if(falling)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - (FallSpeed * Time.deltaTime), this.gameObject.transform.position.z);
            Destroy(this.gameObject, 5.0f);
        }
    }

   public  void playersHaveEntered()
    {
        wallBehind.SetActive(true);
        wallInfront.SetActive(true);
        GameObject Enemy = GameObject.Instantiate(Enemies, SpawnPoint[0].position, SpawnPoint[0].rotation);
        Enemy.GetComponent<EnemyScript>().Spawner = this.gameObject;
        Enemy.GetComponent<EnemyScript>().gameManager = gameManager;
        Enemy.GetComponent<EnemyScript>().playerToFight = Random.Range(0, gameManager.GetComponent<GameManagerScript>().numPlayers);
        GameObject Enemy2 = GameObject.Instantiate(Enemies, new Vector3(SpawnPoint[0].position.x + 5, SpawnPoint[0].position.y, SpawnPoint[0].position.z), SpawnPoint[0].rotation);
        Enemy2.GetComponent<EnemyScript>().Spawner = this.gameObject;

        Enemy2.GetComponent<EnemyScript>().gameManager = gameManager;
        Enemy2.GetComponent<EnemyScript>().playerToFight = Random.Range(0, gameManager.GetComponent<GameManagerScript>().numPlayers);


        foreach (GameObject players in gameManager.GetComponent<GameManagerScript>().currentPlayers)
        {

            if (players != null)
            {
                players.transform.position = movePlayers[players.GetComponent<PlayerControllerOldInput>().playerNum].transform.position;
            }
        }
        LerpTowards = true;
    }

    public void playersHaveExited()
    {
        Debug.Log("Goodbye Blye Sky");
        falling = true;
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
