using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTrajectory : MonoBehaviour 
{
    [SerializeField]private LayerMask m_trajectoryMask;

    private LineRenderer m_trajectoryRenderer;
    private SinkController m_sinkController;

    private void Start()
    {
        if(!m_sinkController)
            m_sinkController = GetComponent<SinkController>();
        if (!m_trajectoryRenderer)
            m_trajectoryRenderer = GetComponent<LineRenderer>();
        SinkTrajectory();
    }

    private void Update()
    {
        if (m_trajectoryRenderer.enabled == true)
            SinkTrajectory();
    }

    private void SinkTrajectory()
    {
        float t = 0.02f; //Time step for calculating trajectory points over time

        RaycastHit hit;

        Vector3 origin = transform.position;
        Vector3 nextPos = origin;
        Vector3 currentPos = origin;

        m_trajectoryRenderer.enabled = false;
        m_trajectoryRenderer.positionCount = 1;
        m_trajectoryRenderer.SetPosition(0, currentPos);

        //If the linecast never hits anything the loup exits after three seconds to avoid an infinite loop
        while (!Physics.Linecast(currentPos, nextPos, out hit, m_trajectoryMask) && t < 3.0f)
        {
            currentPos = nextPos;

            float x = origin.x + m_sinkController.ThrowVelocity.x * t;
            float y = origin.y + (m_sinkController.ThrowVelocity.y*t + ((Physics.gravity.y * t) / 2) * t);
            float z = origin.z + m_sinkController.ThrowVelocity.z * t;
            nextPos = new Vector3(x, y, z);

            m_trajectoryRenderer.positionCount++;
            m_trajectoryRenderer.SetPosition(m_trajectoryRenderer.positionCount - 1, nextPos);

            t += Time.fixedDeltaTime;
        }

        m_trajectoryRenderer.enabled = true;
    }
}
