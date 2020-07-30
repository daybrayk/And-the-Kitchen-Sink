using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManualStartScript : MonoBehaviour {
    public Button startBtn;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Slash))
        {
            startBtn.onClick.Invoke();
        }
	}
}
