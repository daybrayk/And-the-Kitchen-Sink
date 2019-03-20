using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MessageWindowController : MonoBehaviour {
    public GameObject gameOverTxt;
    public GameObject gameStartTxt;
    public Toggle dontShowStartTxt;
    public Canvas messageWindow;
	// Use this for initialization
	void Start () {
        messageWindow = GetComponent<Canvas>();
        if (PlayerPrefs.HasKey("ShowStartTxt"))
            dontShowStartTxt.isOn = PlayerPrefs.GetInt("ShowStartTxt") == 0 ? false : true;
        else
            dontShowStartTxt.isOn = false;
        if(!dontShowStartTxt.isOn)
        {
            messageWindow.enabled = true;
            gameStartTxt.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Close()
    {
        gameOverTxt.SetActive(false);
        gameStartTxt.SetActive(false);
        messageWindow.enabled = false;
    }

    public void OpenOnGameOver()
    {
        messageWindow.enabled = true;
        gameOverTxt.SetActive(true);
    }

    public void OpenOnGameStart()
    {
        if(!dontShowStartTxt)
        {
            messageWindow.enabled = true;
            gameStartTxt.SetActive(true);
        }
    }

    public void SetStartTxtPlayerPref()
    {
        if (!dontShowStartTxt.isOn)
        {
            PlayerPrefs.SetInt("ShowStartTxt", 0);
        }
        else
            PlayerPrefs.SetInt("ShowStartTxt", 1);
    }
}
