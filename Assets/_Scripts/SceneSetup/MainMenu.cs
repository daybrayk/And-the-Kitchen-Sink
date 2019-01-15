using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour {
    public Button start;
    public Button highScore;
    public Button quit;
	// Use this for initialization
	void Start () {
        start.onClick.AddListener(delegate { GameManager.instance.LoadScene("GameScene"); });
        start.onClick.AddListener(delegate { GameManager.instance.QuitGame(); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
