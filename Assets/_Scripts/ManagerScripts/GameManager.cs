using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    #region Game State Variables
    public bool IsGameRunning{ get;  private set; }
    public float HighScore { get; private set; }
    public float Score
    {
        get{ return m_score; }
        
        set
        {
            m_score = value;
            currentscoreText.text = "Currentscore: " + m_score;
            if (m_score > HighScore)
            {
                HighScore = m_score;
                if(highscoreText)
                    highscoreText.text = "Highscore: " + HighScore;
                PlayerPrefs.SetFloat("highscore", HighScore);
            }
        }
    }
    private float m_score;
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

    #region Singleton Reference
    public static GameManager Instance { get; private set; }
    #endregion

    private void Awake()
    {
        #region Singleton Setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        #endregion

        enemyCollector = new List<GameObject>();
        sinkCollector = new List<GameObject>();
        HighScore = PlayerPrefs.GetFloat("highscore");
    }

    private void Start()
    {
        highscoreText.text = "Highscore: " + HighScore;
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
        {
            sinkCollector.Remove(o);
        }
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
        IsGameRunning = true;
    }

    public void StopGame()
    {
        IsGameRunning = false;
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

    }

    public void QuitGame()
    {
        Debug.Log("Closing Game");
        if(Score > HighScore)
            PlayerPrefs.SetFloat("highscore", Score);
        //add persistent data code
        Application.Quit();
    }
}
