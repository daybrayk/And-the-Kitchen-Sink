using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollObject : MonoBehaviour {
    public Rigidbody torso;

    private float m_massTimer;
	// Use this for initialization
	void Start () {
        m_massTimer = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
        m_massTimer -= Time.deltaTime;
        if(m_massTimer < Mathf.Epsilon)
        {
            foreach(Rigidbody rb in GetComponentsInChildren<Rigidbody>())
            {
                rb.mass = 5.0f;
            }
        }
	}

    private void OnEnable()
    {
        Destroy(gameObject, 10.0f);
    }
}
