using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrapnelScript : MonoBehaviour {
    public List<Rigidbody> shrapnel;
    public FragSink sinkReference;
    public float force;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnEnable()
    {
        foreach(Rigidbody rb in shrapnel)
        {
            rb.transform.parent = null;
            rb.isKinematic = false;
            Debug.Log(rb.isKinematic);
            rb.AddExplosionForce(force, transform.position, 0.5f);
        }
        Destroy(gameObject);
    }
}
