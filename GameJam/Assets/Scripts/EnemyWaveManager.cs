using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField] private int minimumEnemiesInGame = 100; //This is the minumum enemies should be in game. If the number of enemies alive equals or exceeds this number, no enemies will be spawning
    [SerializeField] private float timeBetweenWaves = 20; //Time pause between enemy wave spawn
    [SerializeField] private int waveLevel = 1; //This is enemy on spawn number amplifier. numbers of enemies that spawn will equal to waveLevel * 10
    [SerializeField] private int waveLevelIncrease = 2; //number of waves that needs to be spawned before wave level increases
    [SerializeField] private List<EnemyStruct> enemiesList;
    [SerializeField] private List<GameObject> spawnAreasList;

    private bool isSpawnerActive = true; //Condition, which is when active, game will spawn enemies every 60 seconds.
    private int currentWaveNumber = 0; //Number of the wave, when it reaches waveLevelIncrease it goes to zero
    private int totalWaveNumber = 0; //Number of all the waves that were spawned. Reaching certain amounts, game decides to spawn boss wave
    private float timer = 0; //Timer to count, when reaching timeBetweenWaves it activates a spawn, if Spawner is active

    public static int aliveEnemies = 0;

    [SerializeField] private Text waveText;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave();
        waveText=GameObject.Find("WaveCount")?.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (isSpawnerActive && timer >= timeBetweenWaves)
        {
            timer = 0;
            SpawnEnemyWave();
        }
        
    }

    void SpawnEnemyWave()
    {
        timer = 0;

        if (currentWaveNumber != waveLevelIncrease)
            currentWaveNumber += 1;
        else
        {
            currentWaveNumber = 0;
            waveLevel += 1;
        }

        totalWaveNumber += 1;
        if(waveText != null) waveText.text = totalWaveNumber.ToString();

        int enemiesToSpawn = waveLevel * 12;
        int spawnPoint = GetSpawnPoint();

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            EnemyStruct enemy = GetEnemy();
            Instantiate(enemy.enemyPrefab, GetSpawnPos(spawnPoint), Quaternion.identity);
        }

        Debug.Log(aliveEnemies);

        if (totalWaveNumber % 10 == 0)
            SpawnBoss();
    }

    void SpawnBoss()
    {
        Debug.Log("Spawn Boss");
    }

    private EnemyStruct GetEnemy()
    {
        var availableEnemies = enemiesList.Where(e => e.difficulty <= waveLevel).ToList();

        if (availableEnemies.Count == 0)
            availableEnemies.Add(enemiesList[0]); // fallback

        int totalWeight = availableEnemies.Sum(e => e.spawnWeight);
        int randomValue = Random.Range(0, totalWeight);
        int current = 0;

        foreach (var enemy in availableEnemies)
        {
            current += enemy.spawnWeight;
            if (randomValue < current)
            {
                return enemy;
            }
        }

        return availableEnemies[0];
    }

    private int GetSpawnPoint()
    {
        //get player position
        var player = GameObject.FindGameObjectWithTag("Player");
        Vector2 playerPos = player.transform.position; //placeholder
        int chosenPoint = 0;
        float farthestPoint = Vector2.Distance(playerPos, spawnAreasList[chosenPoint].transform.position);
        for (int i = 0; i < spawnAreasList.Count; i++)
        {
            float distance = Vector2.Distance(playerPos, spawnAreasList[i].transform.position);
            if (distance > farthestPoint)
            {
                chosenPoint = i;
                farthestPoint = distance;
            }    
        }

        return chosenPoint;
    }

    private Vector2 GetSpawnPos(int spawnPoint)
    {
        Vector3 spawnCenter = spawnAreasList[spawnPoint].transform.position;
        float spawnRadius = 10f;

        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;

        Vector3 spawnPos = new Vector3(
            spawnCenter.x + randomOffset.x,
            spawnCenter.y + randomOffset.y,
            0
        );

        return spawnPos;
    }

    
}
