using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameScene : MonoBehaviour {
    public Button restart;
    public Button mainMenu;
	// Use this for initialization
	void Start () {
        restart.onClick.AddListener(delegate { GameManager.instance.LoadScene("GameScene"); });
        mainMenu.onClick.AddListener(delegate { GameManager.instance.LoadScene("MainMenu"); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
