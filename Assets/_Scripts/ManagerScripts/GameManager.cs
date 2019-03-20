using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
    [Tooltip("Used to track active enemie gameobjects")] [HideInInspector] public List<GameObject> enemyCollector; //Tracks all active enemies, if the player presses the retart button all sinks are destroyed
    [Tooltip("Used to track active sink gameobjects")] [HideInInspector] public List<GameObject> sinkCollector;  //Tracks all active sinks, if the player presses the restart button all sinks are destroyed
    public Text highscoreText;
    public Text currentscoreText;
    public PlayerController pc;
    public GameScene gs;
    public MessageWindowController messageController;
    [HideInInspector] public bool startGame;
    public EnemySpawner[] eSpawner;
    private float m_score;
    private float m_highscore;
    public float score
    {
        get{ return m_score; }

        set
        {
            m_score = value;
            currentscoreText.text = "Currentscore: " + m_score;
            if (m_score > m_highscore)
            {
                m_highscore = m_score;
                if(highscoreText)
                    highscoreText.text = "Highscore: " + m_highscore;
                PlayerPrefs.SetFloat("highscore", m_highscore);
            }
        }
    }
    private void Awake()
    {

        enemyCollector = new List<GameObject>();
        sinkCollector = new List<GameObject>();
        m_highscore = PlayerPrefs.GetFloat("highscore");
    }

    private void Start()
    {
        highscoreText.text = "Highscore: " + m_highscore;
        currentscoreText.text = "Currentscore: " + m_score;
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
        for(int i = enemyCollector.Count - 1; i >= 0; i--)
        {
            GameObject temp = enemyCollector[i];
            enemyCollector.RemoveAt(i);
            Destroy(temp);
        }
        for(int i = sinkCollector.Count - 1; i >= 0; i--)
        {
            if(sinkCollector[i] != pc.sinkInHands)
            {
                GameObject temp = sinkCollector[i];
                sinkCollector.RemoveAt(i);
                Destroy(temp);

            }
        }
        score = 0;
        pc.ResetHealth();
    }

    public void GameOver()
    {
        messageController.OpenOnGameOver();
        gs.MainMenu();

    }

    public void QuitGame()
    {
        Debug.Log("Closing Game");
        if(score > m_highscore)
            PlayerPrefs.SetFloat("highscore", score);
        //add persistent data code
        Application.Quit();
    }

    public float GetHighscore()
    {
        return m_highscore;
    }
}
