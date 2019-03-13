using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIFunctionality : MonoBehaviour {
    public PlayerController pc;
    [SerializeField] private float attackRange;
    private NavMeshAgent m_aiAgent;
    private Animator m_anim;
	// Use this for initialization
	void Start () {
        m_aiAgent = GetComponent<NavMeshAgent>();
        m_anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if(Time.frameCount % 5 == 0)
        {
            
        }
	}

    public void MoveTo(Vector3 destination)
    {
        m_aiAgent.SetDestination(destination);
        m_anim.SetFloat("speed", m_aiAgent.velocity.sqrMagnitude);
        m_aiAgent.isStopped = false;
    }

    public void Attack()
    {
        m_aiAgent.isStopped = true;
    }

    public void EnterHouse()
    {

    }
}
