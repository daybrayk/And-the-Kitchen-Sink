using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SinkController : MonoBehaviour {
    [HideInInspector]
    public Transform sinkSpawn;
    public float force;
    public float mass;
    public float vx;
    public float vy;
    public float vz;
    public float trajectoryModifier;
    public LineRenderer lr;
    public LayerMask m_enemyMask;

    [SerializeField]
    protected Rigidbody m_rb;
    private Vector3 m_trajectory;
    public Vector3 m_v;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.isKinematic = true;
        if(!lr)
            lr = GetComponent<LineRenderer>();
        mass = m_rb.mass;
        if (trajectoryModifier <= 0)
            trajectoryModifier = 3.0f;
        m_trajectory = new Vector3(transform.forward.x, transform.forward.y * trajectoryModifier, transform.forward.z).normalized;
    }
    protected void Update()
    {
        m_trajectory = new Vector3(transform.forward.x, transform.forward.y * trajectoryModifier, transform.forward.z).normalized;
        m_v = m_trajectory * force / mass;
    }

    protected void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, (Vector3.one * 0.76f) / 2, transform.rotation, m_enemyMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            RagdollScript rs;
            if ((rs = colliders[i].GetComponentInParent<RagdollScript>()))
                Effect(colliders[i], rs);
        }
    }

    protected void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, 0.5f);
        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.8f);
    }
    public void Throw(float force)
    {
        m_rb.isKinematic = false;
        transform.parent = null;
        lr.enabled = false;
        m_rb.AddForce(force * m_trajectory, ForceMode.Impulse);
        Destroy(this, 5.0f);
    }

    /*private void OnCollisionEnter(Collision c)
    {
        Effect(c);
    }*/

    public abstract void Effect(Collider c, RagdollScript rs);
}
