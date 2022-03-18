using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawn : MonoBehaviour
{
    public enum SpawnState{ SPAWNING, WAITING, COUNTING, COMPLETE};
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    StatCount statCount;

    private int nextWave = 0;
    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private SpawnState state = SpawnState.COUNTING;

    private float waveCountdown;
    private float searchCountdown = 1f;
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
