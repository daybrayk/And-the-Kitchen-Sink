using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotIndicator : MonoBehaviour {
    public RaycastProjectileWeapons weapon;
    public LayerMask everything;
    public Rigidbody rb;
    public GameObject indicatorPrefab;
    private float force;
    private float timer;
    private Vector3 rayStart;
    private Vector3 rayEnd;
    private Vector3 wind;
    private float time;
    private float timeStep;
    float v;
    float vx;
    float vy;
    float vz;
    Transform muzzle;
	// Use this for initialization
	void Start () {
        timeStep = 0.001f;
        time = 0f;
        rayStart = rayEnd = weapon.projectileData.muzzle.position;
        muzzle = weapon.projectileData.muzzle;

        force = weapon.projectileData.force;
        wind = weapon.projectileData.windResistance;
        v = force / rb.mass;
        float ry = transform.localEulerAngles.y * Mathf.PI / 180;
        float rx = transform.localEulerAngles.x * Mathf.PI / 180;
        vx = Mathf.Sin(ry) * Mathf.Cos(rx) * v;
        vz = Mathf.Cos(ry) * Mathf.Cos(rx) * v;
        vy = -Mathf.Sin(rx) * v;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        
        //Checks if the loop failed because the ray hit something or because time ran out
        bool trueHit;
        while ((trueHit = !Physics.Linecast(rayStart, rayEnd, out hit, everything)) && time < 1.0)
        {
            Debug.DrawLine(rayStart, rayEnd, Color.blue);
            rayStart = rayEnd;
            float x = muzzle.position.x + ((vx + (wind.x / rb.mass * time / 2)) * time);
            float y = muzzle.position.y + (vy + ((Physics.gravity.y + (wind.y / rb.mass))*time)/2) *time;
            float z = muzzle.position.z + ((vz + (wind.z / rb.mass * time / 2)) * time);
            rayEnd = new Vector3(x, y, z);
            time += timeStep;
        }
        Debug.DrawLine(rayStart, rayEnd, Color.blue);
        if (!trueHit)
        {
            GameObject temp = Instantiate(indicatorPrefab, hit.point,
                indicatorPrefab.transform.rotation);
            Debug.Log("Contact Point: " + hit.point);
            Destroy(temp, 0.03f);
        }
        force = weapon.projectileData.force;
        wind = weapon.projectileData.windResistance;
        v = force / rb.mass;
        float ry = transform.localEulerAngles.y * Mathf.PI / 180;
        float rx = transform.localEulerAngles.x * Mathf.PI / 180;
        vx = Mathf.Sin(ry) * Mathf.Cos(rx) * v;
        vz = Mathf.Cos(ry) * Mathf.Cos(rx) * v;
        vy = -Mathf.Sin(rx) * v;
        time = 0f;
        rayStart = rayEnd = weapon.projectileData.muzzle.position;
    }
}
