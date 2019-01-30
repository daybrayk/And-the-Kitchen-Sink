﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    public static  GameManager instance = null;
    public List<GameObject> enemyCollector;
    public List<GameObject> sinkCollector;
    public float score;
    public bool startGame;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            DestroyImmediate(this);
        enemyCollector = new List<GameObject>();
        sinkCollector = new List<GameObject>();
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("GameScene");
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
        startGame = false;
    }

    public void QuitGame()
    {
        Debug.Log("Closing Game");
        //add persistent data code
        Application.Quit();
    }
}
