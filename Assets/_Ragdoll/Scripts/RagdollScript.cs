using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollScript : MonoBehaviour {

    public GameObject ragdoll;
    public Rigidbody ragdollTorso;
    private Limb[] myLimbs;

	// Use this for initialization
	void Start () {
        myLimbs = GetComponentsInChildren<Limb>();
	}
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateRagdoll();
        }*/
    }

    public void ActivateRagdoll()
    {
        GameManager.instance.RemoveEnemy(gameObject);
        GameManager.instance.AddEnemy(ragdoll);
        foreach (Limb limb in myLimbs)
        {
            limb.SetRagdollPos();
        }
        ragdoll.transform.parent = null;
        ragdoll.SetActive(true);
        Destroy(gameObject, 1.0f);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Overload function for when the bullet hits the toroso. Causes the character to enter
    /// Ragdoll mode and have a force applied to the torso
    /// </summary>
    /// <param name="force"></param>
    public void ActivateRagdoll(Vector3 force)
    {
        foreach(Limb limb in myLimbs)
        {
            limb.SetRagdollPos();
        }
        ragdoll.transform.parent = null;
        ragdoll.SetActive(true);
        ragdollTorso.AddForce(force, ForceMode.Impulse);
            
        gameObject.SetActive(false);
    }
    /// <summary>
    /// Overload function for when the bullet hits a limb that is not the torso.
    /// This causes the limb to seperate from the body and have a force applied to it.
    /// </summary>
    /// <param name="force"></param>
    /// <param name="hitLimb"></param>
    public void ActivateRagdoll(Vector3 force, Limb hitLimb)
    {
        Rigidbody rb = hitLimb.ragdollObject.GetComponent<Rigidbody>();
        //hitLimb.RemoveHingeJoint();
        foreach (Limb limb in myLimbs)
        {
            limb.SetRagdollPos();
        }
        ragdoll.transform.parent = null;
        ragdoll.SetActive(true);
        gameObject.SetActive(false);
        if (rb)
            rb.AddForce(force, ForceMode.Impulse);
    }

    public void ActivateRagdoll(Vector3 force, Limb hitLimb, Vector3 pos)
    {
        Rigidbody rb = hitLimb.ragdollObject.GetComponent<Rigidbody>();
        //hitLimb.RemoveHingeJoint();
        foreach (Limb limb in myLimbs)
        {
            limb.SetRagdollPos();
        }
        ragdoll.transform.parent = null;
        ragdoll.SetActive(true);
        if (rb)
        {

            rb.AddForceAtPosition(force, pos);
        }

        gameObject.SetActive(false);
    }
}
