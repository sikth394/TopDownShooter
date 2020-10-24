using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveSpawner : MonoBehaviour
{

   public enum SpawnState {SPAWNING, WAITING, COUNTING };


   [System.Serializable]
   public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
        public Transform[] enemies; //for spawning multiple types of enemies in the same wave
        public int[] enemiesCount; // array of same length as enemies where each cell holds the number of enemies in the corresponding index in enemies[]
        
    }

    public Wave[] waves;
    public int nextWave = 0;

    public Transform[] spawnPoints;


    public float timeBetweenWaves;
    private float waveCountdown;

    private float searchCountDown = 1f;

    private ItemSpawner itemSpawner;

    public SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points refrenced");
        }
        waveCountdown = timeBetweenWaves;
        itemSpawner = gameObject.GetComponent<ItemSpawner>();
        
    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                BeginNewRound();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                if (waves[nextWave].enemies.Length == 0)
                {
                    Debug.Log("im in the regualar spawn ");
                    StartCoroutine(SpawnWave(waves[nextWave]));

                }
                else
                {

                    StartCoroutine(SpawnWave(waves[nextWave], waves[nextWave].enemies, waves[nextWave].enemiesCount));
                    Debug.Log("im in the  mixed  spawn ");

                }
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }


    void BeginNewRound()
    {
        //Debug.Log("Wave Completed");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1) //end of level code here
        {
            nextWave = 0;
            //Debug.Log("Completed all waves of the level, Looping");
        }
        else
        {
            nextWave += 1;
        }
        itemSpawner.WeaponSpawner(nextWave); //spawn the next weapon pickup according to nextWave, configured in ItemSpawner.

    }

    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave" + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    IEnumerator SpawnWave(Wave _wave, Transform[] enemies, int[] enemiesCount) // overloading of SpawnWave where more then one type of enemy is specified
    {
        Debug.Log("Spawning wave" + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i <enemies.Length; i++)
        {
            for (int j = 0; j < enemiesCount[i]; j++)
            {
                SpawnEnemy(enemies[i]);
                yield return new WaitForSeconds(1f / _wave.rate);
            }
        }
        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        //Debug.Log("Spawning enemy:" + _enemy.name);
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }


}
