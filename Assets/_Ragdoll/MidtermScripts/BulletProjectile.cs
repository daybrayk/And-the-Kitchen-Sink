using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletProjectile : MonoBehaviour {
    public Rigidbody rb;
    public Transform raycastOrigin;
    float distance;
    private Limb temp;
    private Vector3 rayStart;
    private Vector3 rayEnd;
    [SerializeField]
    private float time = 0f;
    [SerializeField]
    private float timeStep = 0.001f;
    [HideInInspector] float damage;
    Vector3 wind;
    [SerializeField] LayerMask layermask;
    private void Start()
    {
        rayStart = rayEnd = raycastOrigin.position;
    }

    private void FixedUpdate()
    {
        
        RaycastHit hit;
        //Checks if the loop failed because the ray hit something or because time ran out
        bool trueHit;
        while ((trueHit = !Physics.Linecast(rayStart, rayEnd, out hit, layermask)) && time < Time.fixedDeltaTime)
        {
            rayStart = rayEnd;
            float x = raycastOrigin.position.x + ((rb.velocity.x + (wind.x / rb.mass * time / 2)) * time);
            float y = raycastOrigin.position.y + (rb.velocity.y + ((Physics.gravity.y + (wind.y / rb.mass))*time) / 2) * time;
            float z = raycastOrigin.position.z + ((rb.velocity.z + (wind.z / rb.mass * time / 2)) * time);
            rayEnd = new Vector3(x, y, z);
            time += timeStep;
        }
        if (!trueHit)
        {
            Debug.LogWarning("Bullet Contact Point: " + hit.point);
            if (hit.collider.gameObject.name == "Torso")
            {
                Debug.Log("Limb Hit: " + hit.collider.gameObject.name);
                RagdollScript playerRef = hit.collider.gameObject.GetComponentInParent<RagdollScript>();
                if (playerRef)
                    playerRef.ActivateRagdoll(rb.mass * rb.velocity / 3);
            }
            else if (temp = hit.collider.gameObject.GetComponent<Limb>())
            {
                Debug.Log("Limb Hit: " + hit.collider.gameObject.name);
                RagdollScript playerRef = hit.collider.gameObject.GetComponentInParent<RagdollScript>();
                if (playerRef)
                    playerRef.ActivateRagdoll(rb.mass * rb.velocity / 3, temp);
            }
            Destroy(gameObject);
        }
        rb.AddForce(wind, ForceMode.Force);
        time = 0f;
        rayStart = rayEnd = raycastOrigin.position;
    }

    public void Launch(float _damage, float _force, float _distance, Vector3 _wind)
    {
        rb.AddRelativeForce(new Vector3(0,0,1) * _force, ForceMode.Impulse);
        
        this.damage = _damage;
        this.distance = _distance;
        this.wind = _wind;
    }
}
