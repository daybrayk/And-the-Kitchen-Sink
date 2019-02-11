using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIFunctionality : MonoBehaviour {
    private NavMeshAgent m_aiAgent;
	// Use this for initialization
	void Start () {
        m_aiAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveTo()
    {

    }
}
