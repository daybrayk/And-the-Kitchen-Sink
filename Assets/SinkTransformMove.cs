using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkTransformMove : StateMachineBehaviour {
    private Rigidbody m_sinkRigidbody;
    private PlayerController m_pc;
    private Vector3 m_throwPosition;
    private float t;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        t = 0;
        m_pc = animator.gameObject.GetComponent<PlayerController>();
        m_throwPosition = m_pc.sinkThrow.position;
        m_sinkRigidbody = m_pc.sinkInHands.GetComponent<Rigidbody>();
        m_pc.canThrow = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        m_sinkRigidbody.position = Vector3.Lerp(m_sinkRigidbody.position, m_throwPosition, t);
        t += Time.fixedDeltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_pc.canThrow = true;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
