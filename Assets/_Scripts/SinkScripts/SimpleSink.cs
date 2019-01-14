using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSink : SinkController {

    

    public override void Effect(Collider c, RagdollScript rs)
    {
        Vector3 collisionForce = GetComponent<Rigidbody>().velocity;
        //rs.ActivateRagdoll(collisionForce * mass, c.GetComponent<Limb>());
        rs.ActivateRagdoll();
    }
}
