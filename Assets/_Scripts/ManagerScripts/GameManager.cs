using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
    #region Game State Variables
    [HideInInspector] public bool isGameRunning;
    private float m_highscore;
    
    private float m_score;
    public float Score
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
    #endregion

    #region UI References
    [Header("UI References")]
    [Tooltip("Reference to the Text object used to display the High Score")][SerializeField]private Text highscoreText;
    [Tooltip("Reference to the Text object used to display the current Score")][SerializeField]private Text currentscoreText;
    [Tooltip("Reference to the script that controlls the start of game instruction window")][SerializeField]private MessageWindowController messageController;
    [Tooltip("Reference to script that controls the UI for the scene")][SerializeField]private GameScene gs;
    #endregion

    #region Player Controller and EnemySpawner References
    [Header("Player Controller")]
    [SerializeField]private PlayerController pc;
    [Header("Enemy Spawners")]
    public EnemySpawner[] eSpawner;
    #endregion


    #region Object Trackers
    [Tooltip("Used to track active enemy Game Objects")] private List<GameObject> enemyCollector; //Tracks all active enemies, if the player presses the retart button all sinks are destroyed
    [Tooltip("Used to track active sink Game Objects")] private List<GameObject> sinkCollector;  //Tracks all active sinks, if the player presses the restart button all sinks are destroyed
    #endregion

    #region Events
    public delegate void GameOver();
    public static GameOver gameOver;
    #endregion

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
        isGameRunning = true;
    }

    public void StopGame()
    {
        isGameRunning = false;
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
            if(sinkCollector[i] != pc.SinkInHands)
            {
                GameObject temp = sinkCollector[i];
                sinkCollector.RemoveAt(i);
                Destroy(temp);

            }
        }
        Score = 0;
        pc.ResetHealth();
    }

    public void OnGameOver()
    {
        if (gameOver != null)
            gameOver();
        /*messageController.Open();
        gs.MainMenu();*/

    }

    public void QuitGame()
    {
        Debug.Log("Closing Game");
        if(Score > m_highscore)
            PlayerPrefs.SetFloat("highscore", Score);
        //add persistent data code
        Application.Quit();
    }

    public float GetHighscore()
    {
        return m_highscore;
    }
}
