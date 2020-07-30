using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragSink : SinkController {
    public ShrapnelScript m_shrapnelEffect;

    public override void ActiveEffect()
    {
        m_shrapnelEffect.transform.position = transform.position + Vector3.up;
        m_shrapnelEffect.transform.parent = null;
        Destroy(gameObject);
        m_shrapnelEffect.gameObject.SetActive(true);
    }

    public override void CollisionEffect(RagdollScript rs)
    {
        rs.ActivateRagdoll();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ActiveEffect();
    }

}
