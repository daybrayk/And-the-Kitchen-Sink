using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameScene : MonoBehaviour {
    public Button restart;
    public Button mainMenu;
    public Button play;
    public Button highscore;
    public Button quit;
    public Button exitHighscore;
    public GameObject mainMenuPanel;
    public GameObject pauseMenuPanel;
    public GameObject highscorePanel;
    [SerializeField]
    private GameManager gm;
	// Use this for initialization
	void Start () {
        restart.onClick.AddListener(delegate { gm.ResetGame(); });
        mainMenu.onClick.AddListener(delegate { MainMenu(); });
        play.onClick.AddListener(delegate { StartGame(); });
        quit.onClick.AddListener(delegate { gm.QuitGame(); });
        highscore.onClick.AddListener(delegate { ShowHighscore(); });
        exitHighscore.onClick.AddListener(delegate { HideHighScore(); });
	}

    public void MainMenu()
    {
        mainMenuPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
        gm.StopGame();
        gm.ResetGame();
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
        gm.ResetGame();
        gm.StartGame();
    }

    public void ShowHighscore()
    {
        highscorePanel.SetActive(true);
    }

    public void HideHighScore()
    {
        highscorePanel.SetActive(false);
    }
}
