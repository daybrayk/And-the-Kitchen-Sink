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
    public GameObject mainMenuPanel;
    public GameObject pauseMenuPanel;
	// Use this for initialization
	void Start () {
        restart.onClick.AddListener(delegate { GameManager.instance.ResetGame(); });
        mainMenu.onClick.AddListener(delegate {  });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MainMenu()
    {
        mainMenuPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
        GameManager.instance.ResetGame();
    }

    public void InGameMenu()
    {
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }
}
