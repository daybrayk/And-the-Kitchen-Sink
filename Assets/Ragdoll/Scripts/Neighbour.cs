using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Neighbour : MonoBehaviour {

    public PlayerData data;
    public RagdollScript rs;
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool TakeDamage(float damage, Vector3 force)
    {
        bool iskilled = false;
        data.health = Mathf.Max(0, data.health - damage);
        Debug.Log(data.health);
        if(data.health <= 0)
        {
            iskilled = true;
            rs.ActivateRagdoll(force);
            data.health += 20;
        }
        return iskilled;
    }

    public void KillPlayer(Vector3 force)
    {
        foreach (Rigidbody rb in transform.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
            rb.transform.parent = null;
            rb.AddForce(force, ForceMode.Impulse);
        }

    }

    public void ProjectileImpact(Vector3 force)
    {
        foreach (Rigidbody rb in transform.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
            rb.transform.parent = null;
            rb.AddForce(force, ForceMode.Impulse);
        }
    }

}
