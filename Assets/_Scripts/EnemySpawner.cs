using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
	// Use this for initialization
	void Start () {
        SpawnEnemy();
	}

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnEnemy()
    {
        GameObject temp = Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation);
        GameManager.instance.AddEnemy(temp);
    }
}
