﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public float activationScore;
    public float spawnCD;
    private float m_spawnCD;
    [SerializeField] private GameManager gm;
    private bool spawnOnce = true;
    private GameObject m_currentEnemySpawned;

    // Update is called once per frame
    void Update()
    {
        /*if(spawnOnce)
        {
            //if(gm.startGame)
            //{
                SpawnEnemy();
                spawnOnce = false;
            //}
        }*/
        if(gm.startGame && gm.score > activationScore && m_spawnCD <= 0)
        {
            SpawnEnemy();
            m_spawnCD = spawnCD;
        }
        m_spawnCD -= Time.deltaTime;
    }

    public/*private*/ void SpawnEnemy()
    {
        m_currentEnemySpawned = Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation);
        m_currentEnemySpawned.GetComponent<AIController>().SetGM(gm);
        gm.AddEnemy(m_currentEnemySpawned);
    }
}
