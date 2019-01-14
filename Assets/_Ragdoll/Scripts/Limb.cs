using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour {
    public Rigidbody ragdollObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void SetRagdollPos()
    {
        ragdollObject.gameObject.transform.position = transform.position;
        ragdollObject.gameObject.transform.rotation = transform.rotation; 
    }

    public void RemoveHingeJoint()
    {
        Destroy(ragdollObject.GetComponent<HingeJoint>());
    }
}
