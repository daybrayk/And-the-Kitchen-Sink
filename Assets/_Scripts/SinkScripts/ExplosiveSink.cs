using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveSink : SinkController {
    public float radius;
    public float explosionForce;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private LayerMask torsoMask;
    public override void ActiveEffect()
    {
        Collider[] objects;
        //StartCoroutine(Explode());
        objects = Physics.OverlapSphere(transform.position, radius, torsoMask);
        Destroy(Instantiate(explosionEffect, transform.position, explosionEffect.transform.rotation), 1.0f);
        Destroy(gameObject);
        foreach (Collider c in objects)
        {

            if (c)
            {
                c.GetComponentInParent<RagdollScript>().ActivateRagdoll(gm);
            }
        }

        objects = Physics.OverlapSphere(transform.position, radius, enemyMask);
        foreach (Collider c in objects)
        {
            Rigidbody rb;
            if ((rb = c.GetComponent<Rigidbody>()))
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius, 1.0f, ForceMode.Impulse);
            }
        }
    }

    public override void CollisionEffect(RagdollScript rs)
    {
        rs.ActivateRagdoll(gm);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ActiveEffect();
    }

    /*private IEnumerator Explode()
    {
        Collider[] objects;
        yield return new WaitForSeconds(4.9f);
        Debug.Log("EXPLODE!!!");
        objects = Physics.OverlapSphere(transform.position, radius, torsoMask);
        Destroy(Instantiate(explosionEffect, transform.position, explosionEffect.transform.rotation), 1.0f);
        foreach(Collider c in objects)
        {
            
            if(c)
            {
                c.GetComponentInParent<RagdollScript>().ActivateRagdoll(gm);
            }
        }

        objects = Physics.OverlapSphere(transform.position, radius, enemyMask);
        foreach(Collider c in objects)
        {
            Rigidbody rb;
            if ((rb = c.GetComponent<Rigidbody>()))
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius, 1.0f, ForceMode.Impulse);
            }
        }
    }*/

    
}
