using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    [SerializeField]
    private GameManager gm;
    private bool spawnOnce = true;

    // Update is called once per frame
    void Update()
    {
        if(spawnOnce)
        {
            if(gm.startGame)
            {
                SpawnEnemy();
                spawnOnce = false;
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject temp = Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation);
        temp.GetComponent<AIController>().SetGM(gm);
        gm.AddEnemy(temp);
    }
}
