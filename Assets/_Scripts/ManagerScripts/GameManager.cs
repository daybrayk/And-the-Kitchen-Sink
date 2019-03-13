using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    [Tooltip("Used to track active enemie gameobjects")] [HideInInspector] public List<GameObject> enemyCollector; //Tracks all active enemies, if the player presses the retart button all sinks are destroyed
    [Tooltip("Used to track active sink gameobjects")] [HideInInspector] public List<GameObject> sinkCollector;  //Tracks all active sinks, if the player presses the restart button all sinks are destroyed
    public float score
    {
        get{ return score; }

        set
        {
            score = value;
            if (score > m_highscore)
            {
                m_highscore = score;
            }
        }
    }
    [HideInInspector] public bool startGame;
    public EnemySpawner[] eSpawner;
    private float m_highscore;
    private void Awake()
    {

        enemyCollector = new List<GameObject>();
        sinkCollector = new List<GameObject>();
    }

    public void AddSink(GameObject o)
    {
        if (!sinkCollector.Contains(o))
        {
            Debug.Log("Adding " + o.name + " to sinkCollector");
            sinkCollector.Add(o);
        }
    }

    public void RemoveSink(GameObject o)
    {
        if (sinkCollector.Contains(o))
            sinkCollector.Remove(o);
    }

    public void AddEnemy(GameObject o)
    {
        if(!enemyCollector.Contains(o))
        {
            Debug.Log("Adding " + o.name + " to enemyCollector");
            enemyCollector.Add(o);
        }
    }

    public void RemoveEnemy(GameObject o)
    {
        if (enemyCollector.Contains(o))
            enemyCollector.Remove(o);
    }

    public void StartGame()
    {
        startGame = true;
    }

    public void StopGame()
    {
        startGame = false;
    }

    public void ResetGame()
    {
        Debug.Log(enemyCollector.Count);
        while(enemyCollector.Count > 0)
        {
            GameObject temp = enemyCollector[0];
            enemyCollector.RemoveAt(0);
            Destroy(temp);
        }
        while(sinkCollector.Count > 0)
        {
            GameObject temp = sinkCollector[0];
            sinkCollector.RemoveAt(0);
            Destroy(temp);
        }
        score = 0;
        /********** Temporary Spawner for Alpha Build **********/
        foreach(EnemySpawner e in eSpawner)
        {
            e.SpawnEnemy();
        }
        /********** Temporary Spawner for Alpha Build **********/
    }

    public void QuitGame()
    {
        Debug.Log("Closing Game");
        if(score > m_highscore)
            PlayerPrefs.SetFloat("highscore", score);
        //add persistent data code
        Application.Quit();
    }
}
