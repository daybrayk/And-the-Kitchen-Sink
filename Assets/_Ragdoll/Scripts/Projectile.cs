using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public Rigidbody rb;

    [HideInInspector] float damage;
    [SerializeField] float explosionRadius;
    [SerializeField] float explosionForce;
    [SerializeField] float explosionUpwardMod;

    [SerializeField] LayerMask layermask;

    [SerializeField] GameObject explosionEffectPrefab;

    List<Neighbour> allPlayerHitten = new List<Neighbour>();

    public void Launch(float _damage, float _force)
    {
        rb.AddForce(transform.forward * _force, ForceMode.Impulse);
        this.damage = _damage;
    }


    private void OnCollisionEnter(Collision _collision)
    {
        GameObject exp = Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        exp.transform.localScale = new Vector3(explosionRadius * 2, explosionRadius * 2, explosionRadius * 2);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in hitColliders)
        {
            Neighbour player = collider.GetComponentInParent<Neighbour>();

            if(player != null)
            {
                RegisterHittenPlayer(player);
            }
        }

        foreach (Neighbour playerHitten in allPlayerHitten)
        {
            playerHitten.TakeDamage(damage, (playerHitten.transform.position - transform.position).normalized * explosionForce);
        }

        Destroy(exp, 0.2f);
        Destroy(gameObject);
    }

    void RegisterHittenPlayer(Neighbour _player)
    {
        if(allPlayerHitten.Contains(_player))
        {
            return;
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position - transform.forward * 0.2f, (_player.transform.position + _player.transform.up * 0.7f - transform.position), out hit, explosionRadius , layermask))
            {
                if (hit.transform.GetComponentInParent<Neighbour>() == _player)
                {
                    Debug.Log("Hit player");
                    allPlayerHitten.Add(_player);
                }
                else
                {
                    Debug.Log("Missed");
                }
            }

            
        }
    }

}
