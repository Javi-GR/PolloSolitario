using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawn : MonoBehaviour
{
    public GameObject enemy;
    public Transform player;

    StatCount statCount;

    private int amountRounds = 3;
    private int enemySpawnrate = 2;
    private int enemiesPerRound = 10;
    private bool spawnStart = false;
    // Start is called before the first frame update
   
    void Start()
    {
        statCount = GameObject.FindGameObjectWithTag("HUD").GetComponent<StatCount>();
        statCount.DisplayRound();
    }
    // Update is called once per frame
    void Update()
    {
        if(spawnStart == false && Vector3.Distance(player.position, gameObject.transform.position)<3f){
            StartCoroutine(SpawnEnemies());
        }
    }
    private IEnumerator SpawnEnemies(){
        spawnStart = true;
        int spawnedEnemies = 0;
        while(spawnedEnemies<=enemiesPerRound){
            if(spawnedEnemies == 10 && statCount.GetRounds()<4){
               statCount.AddRound();
               spawnedEnemies = 0;
               yield return new WaitForSeconds(2);
               
            }if(statCount.GetRounds()==4){
                statCount.LevelCompleted();
                break;
            }
            SpawnSingleEnemy();
            spawnedEnemies++;
            
            yield return new WaitForSeconds(3);
        }
        
    }
    void SpawnSingleEnemy(){
        GameObject newEnemy = Instantiate(enemy, gameObject.transform.position, Quaternion.identity);

    }
}
