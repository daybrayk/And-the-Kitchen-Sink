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
    [SerializeField]
    protected Rigidbody _rb;
    protected float v;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
        if(!lr)
            lr = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        float ry = transform.eulerAngles.y * Mathf.PI / 180;
        float rx = transform.eulerAngles.x * Mathf.PI / 180;
        vx = Mathf.Sin(ry) * Mathf.Cos(rx) * v;
        vz = Mathf.Cos(ry) * Mathf.Cos(rx) * v;
        vy = -Mathf.Sin(rx) * v;
    }
    public void Throw(Vector3 force)
    {
        _rb.isKinematic = false;
        transform.parent = null;
        lr.enabled = false;
        _rb.AddForce(force, ForceMode.Impulse);
        Destroy(this, 5.0f);
    }

    public void AdjustForce(float force)
    {
        v = force / _rb.mass;
    }

    private void OnCollisionEnter(Collision c)
    {
        Effect(c);
    }

    public abstract void Effect(Collision c);
}
