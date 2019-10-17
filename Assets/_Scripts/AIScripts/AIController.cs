using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIController : MonoBehaviour
{
    //TODO: Implement enemy's attack

    #region Object References
    [Header("Object References")]
    private GameManager m_gameManager;
    private Transform m_currentDestination;
    private Transform m_houseDoorTransform;
    #endregion

    #region AI
    [Header("AI")]
    private NavMeshAgent m_aiAgent;
    private AIFunctionality m_aiFunctionality;

    #endregion
	
	void Start () {
        m_aiFunctionality = GetComponent<AIFunctionality>();
        m_aiAgent = GetComponent<NavMeshAgent>();

        m_houseDoorTransform = GameObject.Find("HouseDoor").transform;

        m_currentDestination = m_houseDoorTransform;
    }

    void Update () {
        m_aiFunctionality.MoveTo(m_currentDestination.position);
	}

    private void OnDestroy()
    {
        if (!m_gameManager)
            return;

        m_gameManager.Score += 10f;
        m_gameManager.RemoveEnemy(gameObject);
    }

    public void SetGM(GameManager gm)
    {
        m_gameManager = gm;
        
    }
}
