using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {
    private GameManager gm;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy()
    {
        gm.RemoveEnemy(gameObject);
    }

    public void SetGM(GameManager gm)
    {
        this.gm = gm;
    }
}
