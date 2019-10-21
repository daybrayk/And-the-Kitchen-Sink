using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Rigidbody))]
public abstract class SinkController : MonoBehaviour
{
    protected const int COLLIDER_CACHE_SIZE = 32;
    private Collider[] m_colliderCache;

    [SerializeField]protected float lifeSpan;

    #region Layer Masks
    [SerializeField]protected LayerMask enemyMask;
    protected LayerMask terrainMask;
    #endregion

    #region Trajectory Calculation Variables
    protected float force;
    protected float mass;
    protected Vector3 m_trajectory;//Current direction of the throw
    public Vector3 ThrowVelocity
    {
        get { return m_trajectory * force/mass; }
    }
    #endregion

    #region Component References
    protected Rigidbody m_rigidBody;
    protected LineRenderer m_lineRenderer;
    #endregion

    #region Object References
    protected Transform m_sinkSpawn;
    protected Camera main;
    //protected GameManager m_gameManager;
    #endregion

    private void Awake()
    {
        terrainMask = (~1 << 10 | ~1 << 12 | ~1 << 13);
        main = Camera.main;
        m_rigidBody = GetComponent<Rigidbody>();
        m_lineRenderer = GetComponent<LineRenderer>();
        mass = m_rigidBody.mass;
        m_colliderCache = new Collider[COLLIDER_CACHE_SIZE];
    }

    protected void Update()
    {
        if(m_lineRenderer)
        {
            Ray ray = new Ray(main.transform.position, main.transform.forward);
            RaycastHit hit;
            m_trajectory = Physics.Raycast(ray, out hit, terrainMask) ? Vector3.Normalize(hit.point - m_sinkSpawn.position) :
                                     Vector3.Normalize(ray.GetPoint(500.0f) - m_sinkSpawn.position);
        }
    }

    protected void FixedUpdate()
    {
        Physics.OverlapBoxNonAlloc(transform.position, (Vector3.one * 0.76f * transform.localScale.x) / 2, m_colliderCache, transform.rotation, enemyMask);
        foreach(Collider c in m_colliderCache)
        {
            if(c)
            {
                RagdollScript rs;
                if ((rs = c.GetComponentInParent<RagdollScript>()))
                    CollisionEffect(rs);
            }
        }
    }

    public void SinkConstructor(Transform sinkSpawn, float throwPower)
    {
        if(GameManager.Instance)
            GameManager.Instance.AddSink(gameObject); //Add sink to the GameManager sink tracker
        m_sinkSpawn = sinkSpawn;
        force = throwPower;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + (Vector3.up * 0.25f), new Vector3(0.9f, 0.4f, 0.8f));
    }

    public void Throw(float force)
    {
        if (!m_lineRenderer)
            m_lineRenderer = GetComponent<LineRenderer>();//Line Renderer is a required component so no need to check if it is there

        if (!m_rigidBody)
            m_rigidBody = GetComponent<Rigidbody>();//Rigidbody is a required component so no need to check if it is there

        transform.parent = null;

        m_lineRenderer.enabled = false;

        m_rigidBody.isKinematic = false;
        m_rigidBody.AddForce(force * m_trajectory, ForceMode.Impulse);
        m_rigidBody.AddTorque(new Vector3(UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 5)), ForceMode.Impulse);  //Adds a random rotation to the sink before it is thrown to simulate a realistic throw

        Destroy(gameObject, lifeSpan);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance)
            GameManager.Instance.RemoveSink(gameObject);
        else
            Debug.LogError("Missing reference to GameManager Instance");
    }

    public abstract void CollisionEffect(/*Collider c, */RagdollScript rs);
    public abstract void ActiveEffect();

    /*public void SetGM(GameManager gm)
    {
        m_gameManager = gm;
    }

    public GameManager GetGM()
    {
        return m_gameManager;
    }*/
}
