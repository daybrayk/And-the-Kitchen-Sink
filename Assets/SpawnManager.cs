using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public List<EnemySpawner> spawners;
    public float spawnCD;
    public GameManager gm;
    [Tooltip("A short cooldown that staggers the enemy spawn times")] public float globalCD;
    [Tooltip("The score at which the number of enemies spawned will increase")] public int spawnIncreaseThreshold;
    private int numberOfEnemiesSpawned;
    private float m_spawnCD;
    private float m_globalCD;
    private bool m_activateOnce;
    private int enemyCount;
	// Use this for initialization
	void Start () {
        numberOfEnemiesSpawned = 2;
        m_activateOnce = true;
	}
	
	// Update is called once per frame
	void Update () {
        if(gm.Score % 100 == 0 && !m_activateOnce)
        {
            numberOfEnemiesSpawned = Mathf.Clamp(numberOfEnemiesSpawned++, 0, 5);
        }
        else if (gm.Score % 100 != 0)
        {
            m_activateOnce = true;
        }

        
        if(m_spawnCD <= 0 && gm.isGameRunning)
        {
            if(enemyCount < numberOfEnemiesSpawned)
            {
                Debug.Log("Enemy should spawn");
                if(m_globalCD <= 0)
                {
                    spawners[Random.Range(0, spawners.Count)].SpawnEnemy();
                    m_globalCD = globalCD;
                }
                else
                    m_globalCD -= Time.deltaTime;
                enemyCount++;
            }
            else
            {
                m_spawnCD = spawnCD;
                enemyCount = 0;
            }
        }
        else
            m_spawnCD -= Time.deltaTime;
    }
}
