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
    public LineRenderer lr;
    public LayerMask m_enemyMask;

    [SerializeField]
    protected Rigidbody m_rb;
    protected float m_v;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.isKinematic = true;
        if(!lr)
            lr = GetComponent<LineRenderer>();
        mass = m_rb.mass;
    }
    protected void Update()
    {
        float ry = transform.eulerAngles.y * Mathf.PI / 180;
        float rx = transform.eulerAngles.x * Mathf.PI / 180;
        vx = Mathf.Sin(ry) * Mathf.Cos(rx) * m_v;
        vz = Mathf.Cos(ry) * Mathf.Cos(rx) * m_v;
        vy = -Mathf.Sin(rx) * m_v;
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
    public void Throw(Vector3 force)
    {
        m_rb.isKinematic = false;
        transform.parent = null;
        lr.enabled = false;
        m_rb.AddForce(force, ForceMode.Impulse);
        Destroy(this, 5.0f);
    }

    public void AdjustForce(float force)
    {
        m_v = force / mass;
    }

    /*private void OnCollisionEnter(Collision c)
    {
        Effect(c);
    }*/

    public abstract void Effect(Collider c, RagdollScript rs);
}
