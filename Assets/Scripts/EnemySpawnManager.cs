using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class EnemySpawnManager : NetworkBehaviour
{
    [SerializeField] private Transform sivionEnemySpawnPoints;
    [SerializeField] private Transform donusEnemySpawnPoints;
    [SerializeField] private GameObject enemyPrefab;

    private GameObject localPlayer, coopPlayer;
    private float timerAcc = 0f, spawnInterval = 10;
    private bool startTimer = false;
    private int totalEnemies = 1; // enemies per side, so 1 = 2 enemies in scene
    private int enemiesInScene = 0;
    public override void OnNetworkSpawn()
    {
        startTimer = getStartTimer;
    }

    private void Update()
    {
        if (!IsServer) return;
        if (!startTimer)
        {
            startTimer = getStartTimer;
            return;
        }
        timerAcc += Time.deltaTime;

        if (timerAcc >= spawnInterval && enemiesInScene == 0)
        {
            spawnEnemies();
            timerAcc= 0f;
        }
        
    }

    private void spawnEnemies()
    {
        Debug.Log("spawn enemies");
        enemiesInScene = 0;
        for (int i = 0; i < totalEnemies; i++)
        {
            int sivionEnemy = Random.Range(0,sivionEnemySpawnPoints.childCount);
            int donusEnemy = Random.Range(0, donusEnemySpawnPoints.childCount);
            spawnEnemy(sivionEnemy, true);
            spawnEnemy(donusEnemy, false);
            enemiesInScene += 2;
        }
    }
    public void SubtractEnemies()
    {
        enemiesInScene--;
        Debug.Log(enemiesInScene);
    }
    private void spawnEnemy(int spawnPoint, bool isSivion)
    {
        Vector3 enemySpawnPos;
        Quaternion enemySpawnRot;
        if (isSivion)
        {
             enemySpawnPos = sivionEnemySpawnPoints.GetChild(spawnPoint).position;
             enemySpawnRot = sivionEnemySpawnPoints.GetChild(spawnPoint).rotation;
        }
        else
        {
             enemySpawnPos = donusEnemySpawnPoints.GetChild(spawnPoint).position;
             enemySpawnRot = donusEnemySpawnPoints.GetChild(spawnPoint).rotation;
        }
        GameObject enemy = Instantiate(enemyPrefab, enemySpawnPos, enemySpawnRot);

        NetworkObject enemyNetObj = enemy.GetComponent<NetworkObject>();
        enemy.SetActive(true);
        enemyNetObj.Spawn();
        enemy.GetComponent<Enemy_Logistics>().InitEnemySpawnManager(this);
    }
    private bool getStartTimer { get { return GameObject.FindGameObjectsWithTag("Player").Count() == 2; } }
    
}
