﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public PlayerHealth playerHeath;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

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
        
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}