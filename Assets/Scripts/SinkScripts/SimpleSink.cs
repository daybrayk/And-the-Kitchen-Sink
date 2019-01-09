using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSink : SinkController {
    public override void Effect(Collision c)
    {
        RagdollScript rs;
        Vector3 collisionForce = GetComponent<Rigidbody>().velocity;

        if (rs = c.gameObject.GetComponentInParent<RagdollScript>())
        {
            rs.ActivateRagdoll(collisionForce * mass, c.gameObject.GetComponent<Limb>());
        }
    }
}
