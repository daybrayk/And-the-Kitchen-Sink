using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIFunctionality : MonoBehaviour 
{
    private NavMeshAgent m_navAgent;
    private Animator m_anim;

	void Awake () {
        m_navAgent = GetComponent<NavMeshAgent>();
        m_anim = GetComponent<Animator>();
	}
	
	void Update () {

        if(Time.frameCount % 5 == 0)//Execute every 5 frames
        {
            
        }
	}

    public void Move()
    {
        m_navAgent.isStopped = false;
        m_anim.SetFloat("speed", m_navAgent.velocity.sqrMagnitude);
    }

    public void MoveTo(Vector3 destination)
    {
        m_navAgent.SetDestination(destination);
        m_navAgent.isStopped = false;
        m_anim.SetFloat("speed", m_navAgent.velocity.sqrMagnitude);
    }


    public void Stop()
    {
        m_navAgent.isStopped = true;
        m_anim.SetFloat("speed", 0);
    }

    public void Attack()
    {
        m_navAgent.isStopped = true;
        m_anim.SetTrigger("wave");
    }

    public NavMeshAgent GetAgent()
    {
        return m_navAgent;
    }
}
