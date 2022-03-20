using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawn : MonoBehaviour
{
    //Enums that represents the current state of the round
    public enum SpawnState{ SPAWNING, WAITING, COUNTING, COMPLETE};

    //Wave class that makes it really easy to create a new wave with the rounds, enemies, rate and count you want
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }
    //Array that represents the amount of waves in the level
    public Wave[] waves;
    //Statcount in the HUD reference
    StatCount statCount;
    private int nextWave = 0;
    //Transforms of where the enmy spawns
    public Transform[] spawnPoints;
    //Time between each wave
    public float timeBetweenWaves = 5f;
    //The state that is associated by default
    private SpawnState state = SpawnState.COUNTING;
    //To make the countdown between each round
    private float waveCountdown;
    //This is to check if an enemy is alive each 1 second,
    // so we dont oversaturate the update method calling each frame to look for an alive enemy
    private float searchCountdown = 1f;
    //The audio played after each round
    public AudioSource audioRound;

    void Start()
    {
        
        statCount = GameObject.FindGameObjectWithTag("HUD").GetComponent<StatCount>();
        if(statCount == null)
        {
            Debug.Log("No HUD found");
        }
        else{
            statCount.DisplayRound();
        }
        
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if(state == SpawnState.WAITING)
        {
            
            if(!EnemyIsAlive())
            {
               WaveCompleted();
            }
            else
            {
                return;
            }
        }
        if(waveCountdown<=0 )
        {
            if(state!=SpawnState.SPAWNING && state!=SpawnState.COMPLETE)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
            
        }
        if(state==SpawnState.COMPLETE)
        {
            return;
        }
        else
        {
            waveCountdown -=Time.deltaTime;
        }
    }
    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");
        audioRound.Play();
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave+1 >waves.Length -1)
        {
            //HERE IS WHERE THE GAMESTATE IS ACTUALLY COMPLETE
            statCount.LevelCompleted();
            nextWave = 0;
            state = SpawnState.COMPLETE;
            GameObject fences = GameObject.FindGameObjectWithTag("DestroyableFences");
            Destroy(fences);
        }else
        {
            nextWave++;
            statCount.AddRound();
        }
       
    }

    bool EnemyIsAlive()
    {
        searchCountdown-= Time.deltaTime;
        if(searchCountdown<=0f)
        {
            searchCountdown = 1f;
            if(GameObject.FindGameObjectWithTag("SpawningEnemy")==null)
            {
                Debug.Log("Enemy found alive, continue killing");
                return false;
            }
        }
        
        return true;
    }
    

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave "+_wave.name);
        state = SpawnState.SPAWNING;

        for(int i = 0; i<_wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/_wave.rate);
        }
            
        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        
        Debug.Log("Spawning enemy: "+_enemy.name);

       
        Transform sp = spawnPoints[ Random.Range(0, spawnPoints.Length) ];
        Instantiate(_enemy, sp.position, sp.rotation);
    }
    
  
}
