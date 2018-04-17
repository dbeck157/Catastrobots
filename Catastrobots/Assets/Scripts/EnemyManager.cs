using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public PlayerHealth playerHeath;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public int enemyCount;

    private void Start()
    {
        InvokeRepeating("Spawn", 0f, spawnTime);
    }

    void Spawn()
    {
        if (playerHeath.currentHealth <= 0f)
        {
            return;
        }

        enemyCount = Random.Range(1, 5);
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemy, new Vector3(spawnPoints[spawnPointIndex].position.x + Random.Range(-2f, 2f),
                                            spawnPoints[spawnPointIndex].position.y,
                                            spawnPoints[spawnPointIndex].position.z + Random.Range(-2f, 2f)), spawnPoints[spawnPointIndex].rotation);
        }
    }
}
