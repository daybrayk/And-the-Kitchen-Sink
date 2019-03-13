using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {
    public PlayerController pc;
    public float destinationChangeTimer;
    private float m_destinationChangeTimer;
    private GameManager gm;
    private AIFunctionality m_aiFunctionality;
	// Use this for initialization
	void Start () {
        m_destinationChangeTimer = destinationChangeTimer;
        m_aiFunctionality = GetComponent<AIFunctionality>();
	}
	
	// Update is called once per frame
	void Update () {
        m_destinationChangeTimer -= Time.deltaTime;
        if(m_destinationChangeTimer <= 0)
        {
            if(pc.currentBunker.name == "HouseBunker")
            {
                //move toward the door of the house
            }
            else
            {
                Transform t = pc.currentBunker.playerPosition.transform;
                m_aiFunctionality.MoveTo(t.position);
            }
        }
	}

    private void OnDestroy()
    {
        gm.score += 10f;
        gm.RemoveEnemy(gameObject);
    }

    public void SetGM(GameManager gm)
    {
        this.gm = gm;
    }
}
