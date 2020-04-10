using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script di gestione della singola Wave
 */
public class Wave : MonoBehaviour {

    [SerializeField] int numberOfSpawnPoints = 20;          // Numero di spawnPoints da generare
    private GameObject[] SpawnPoints;                       // Gli spawn points generati e memorizzati in array
    public GameObject spawnPointPrefab;                     // Il prefab di spawnpoints da generare/usare
    [SerializeField] int wavePointsReward = 100;            // Valore in punti/score/valuta che il player riceve a fine wave
    [SerializeField] GameObject[] AllowedEnemyToSpawn;      // Tipologie di nemici che compongono la wave
    [SerializeField] int waveDifficulty;                    // Difficoltà totale della wave
    private int reachedDifficulty;                          // Difficoltà raggiunta con in nemici spawnati fino ad un certo istante di tempo
    [SerializeField] float timeBetweenSpawns;               // Tempo tra 1 spawn e il successivo
    private bool spawnProcessIsActive = true;               // true se sta ancora spawnando i nemici.

    [SerializeField] bool debug = false;                    // Debug = true --> console debugging

    // Child sub-parents //
    public GameObject spawn_parent;
    public GameObject enemies_parent;

    void Start () {
        GenerateSpawnPoints();
        StartCoroutine(spawnNextEnemy());	
	}
	
	void Update () {
        CheckIfCompleted();
	}

    // Genera "numberOfSpawnPoints" spawn points che verranno successivamente utilizzati come punti di spawn per i nemici.
    void GenerateSpawnPoints()
    {
        SpawnPoints = new GameObject[numberOfSpawnPoints];
        for(int i = 0; i < numberOfSpawnPoints; i++)
        {
            GameObject instantiated = Instantiate(spawnPointPrefab, transform.position, transform.rotation) as GameObject;
            SpawnPoints[i] = instantiated;
            instantiated.transform.SetParent(spawn_parent.transform);
        }
        if (debug)
        {
            Debug.Log("All Spawn Points Generated! Starting to spawn enemies!");
        }
    }

    // Coroutine di spawn dei nemici.
    // -> waita "timeBetweenSpawns"
    // -> Sceglie a random uno spawnPoint
    // -> Sceglie a random cosa spawnare
    // -> Spawna quell'entità nello spawnPoint

    IEnumerator spawnNextEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            if (reachedDifficulty < waveDifficulty)
            {
                int nextSpawnIndex = Random.Range(0, numberOfSpawnPoints);
                int nextEnemyIndex = Random.Range(0, AllowedEnemyToSpawn.Length);
                GameObject nextSpawn = SpawnPoints[nextSpawnIndex];
                GameObject nextEnemy = AllowedEnemyToSpawn[nextEnemyIndex];

                GameObject instantiated = Instantiate(nextEnemy, nextSpawn.transform.position, transform.rotation);
                var spawned_value = nextEnemy.GetComponent<Enemy>().difficultyValue;
                reachedDifficulty = reachedDifficulty + spawned_value;
                instantiated.transform.SetParent(enemies_parent.transform);

                if (debug)
                {
                    Debug.Log("Wave: Enemy Instantiated with streght value of: " + spawned_value + ". Reached status [" + reachedDifficulty + "/" + waveDifficulty+"]");
                }
            }
            else
            {
                spawnProcessIsActive = false;
                if (debug)
                {
                    Debug.Log("Wave: Spawning process completed!");
                }
            }
        }
    }

    // Controlla se la wave è completa (finito di spawnare e zero nemici vivi) e allora starta la next (chiamando startNextWave() del wavesmanager) e si distrugge.
    void CheckIfCompleted()
    {
        int enemiesAlive = enemies_parent.transform.childCount;

        if(spawnProcessIsActive == false)
        {
            if(enemiesAlive > 0)
            {
                if (debug)
                {
                    Debug.Log("Wave: waiting player to kill " +enemiesAlive+ " enemies in order to complete the wave!!");
                }
            }else if(enemiesAlive <= 0)
            {
                if (debug)
                {
                    Debug.Log("Wave: COMPLETED - Next wave will starts soon!");
                }
                this.gameObject.transform.parent.GetComponent<WavesManager>().NextWavePhaseHandler(false,true);
                GameObject scoremanager = GameObject.FindGameObjectWithTag("ScoreManager");
                scoremanager.GetComponent<ScoreManager>().addScore(wavePointsReward);
                Destroy(this.gameObject);
            }
        }
    }

}
