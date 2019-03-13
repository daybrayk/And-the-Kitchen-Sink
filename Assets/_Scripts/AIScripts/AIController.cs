using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIController : MonoBehaviour {
    public PlayerController pc;
    public float destinationChangeTimer;
    public float attackCD;
    private NavMeshAgent m_aiAgent;
    private float m_destinationChangeTimer;
    private GameManager gm;
    private AIFunctionality m_aiFunctionality;
    private Transform m_currentDestination;
    private float m_attackCD;
    private Transform m_houseDoorTransform;
	// Use this for initialization
	void Start () {
        m_aiFunctionality = GetComponent<AIFunctionality>();
        m_aiAgent = GetComponent<NavMeshAgent>();
        /*************** Temporary ***************/
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        m_houseDoorTransform = GameObject.Find("HouseDoor").transform;
        /*************** Temporary ***************/
    }

    // Update is called once per frame
    void Update () {
        m_destinationChangeTimer -= Time.deltaTime;
        if(m_destinationChangeTimer <= 0)
        {
            if(pc.currentBunker.name == "HouseBunker")
            {
                //move toward the door of the house
                if(m_houseDoorTransform)
                {
                    m_currentDestination = m_houseDoorTransform;
                    m_aiFunctionality.MoveTo(m_currentDestination.position);
                }
            }
            else
            {
                m_currentDestination = pc.currentBunker.playerPosition.transform;
                m_aiFunctionality.MoveTo(m_currentDestination.position);
            }
            m_destinationChangeTimer = destinationChangeTimer;
        }
        if(m_currentDestination)
        {
            if (Vector3.Distance(m_currentDestination.position, transform.position) < m_aiFunctionality.GetAgent().stoppingDistance && m_attackCD <= 0)
            {
                m_aiFunctionality.Attack();
                pc.TakeDamage(10);
                m_attackCD = attackCD;
            }
            else
                m_attackCD -= Time.deltaTime;

        }
	}

    private void OnDestroy()
    {
        if(gm)
        {
            gm.score += 10f;
            gm.RemoveEnemy(gameObject);

        }
    }

    public void SetGM(GameManager gm)
    {
        this.gm = gm;
        
    }
}
