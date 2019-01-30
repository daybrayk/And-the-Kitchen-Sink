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
    public LayerMask enemyMask;
    public LayerMask terrainMask;
    public Vector3 m_v;
    public Camera main;
    protected Rigidbody m_rb;
    protected Collider[] colliderCache;
    protected int colliderCacheSize = 32;
    private Vector3 m_trajectory;
    [SerializeField]
    protected GameManager gm;

    private void Awake()
    {
        terrainMask = (~1 << 10 | ~1 << 12 | ~1 << 13);
        main = Camera.main;
        m_rb = GetComponent<Rigidbody>();
        m_rb.isKinematic = true;
        if(!lr)
            lr = GetComponent<LineRenderer>();
        mass = m_rb.mass;
        if (trajectoryModifier <= 0)
            trajectoryModifier = 3.0f;
        m_trajectory = new Vector3(transform.forward.x, 0.60f, transform.forward.z).normalized;
    }

    protected void Update()
    {

        Ray ray = new Ray(main.transform.position, main.transform.forward);
        RaycastHit hit;
        m_trajectory = Physics.Raycast(ray, out hit, terrainMask) ? Vector3.Normalize(hit.point - sinkSpawn.position) :
                                 Vector3.Normalize(ray.GetPoint(500.0f) - sinkSpawn.position);
        m_v = m_trajectory * force / mass;
    }

    protected void FixedUpdate()
    {
        //Collider[] colliders = Physics.OverlapBox(transform.position, (Vector3.one * 0.76f) / 2, transform.rotation, m_enemyMask);
        Physics.OverlapBoxNonAlloc(transform.position, (Vector3.one * 0.76f) / 2, ColliderCache, transform.rotation, enemyMask);
        foreach(Collider c in colliderCache)
        {
            if(c)
            {
                RagdollScript rs;
                if ((rs = c.GetComponentInParent<RagdollScript>()))
                    Effect(c, rs);
            }
        }
    }

    protected void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, 0.5f);
        Gizmos.DrawWireCube(transform.position + (Vector3.up * 0.25f), new Vector3(0.9f, 0.4f, 0.8f));
    }
    public void Throw(float force)
    {
        m_rb.isKinematic = false;
        transform.parent = null;
        lr.enabled = false;
        m_rb.AddForce(force * m_trajectory, ForceMode.Impulse);
        m_rb.AddTorque(new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5)), ForceMode.Impulse);
        Destroy(gameObject, 5.0f);
    }

    private void OnDestroy()
    {
        gm.RemoveSink(gameObject);
    }

    public Collider[] ColliderCache
    {
        get
        {
            if (colliderCache == null)
                colliderCache = new Collider[colliderCacheSize];
            return colliderCache;
        }
    }

    public abstract void Effect(Collider c, RagdollScript rs);

    public void SetGM(GameManager gm)
    {
        this.gm = gm;
    }
}
