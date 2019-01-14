using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RaycastWeaponData: WeaponData
{
    public float range;
}

public class RaycastWeapon : Weapon {

    public RaycastWeaponData raycastData;

    protected override void Start()
    {
        data = raycastData;
        base.Start();
    }

    // Write Raycast Script here
    protected override void LaunchProjectile()
    {
        CalDirection();

        RaycastHit hit;
        if (Physics.Raycast(raycastData.muzzle.position, raycastData.muzzle.transform.forward, out hit, raycastData.range, ~raycastData.myPlayerLayer))
        {
            Debug.DrawRay(raycastData.muzzle.position, raycastData.muzzle.transform.forward * hit.distance, Color.red);
            Debug.Log("Did Hit");

            var resultHitbox = hit.transform.GetComponent<HitBox>();
            if (resultHitbox != null)
            {
                if (resultHitbox.Hit(raycastData.damage))
                {    
                    hit.transform.parent = null;
                    hit.transform.GetComponent<Rigidbody>().AddForce(-hit.normal * 10f, ForceMode.Impulse);
                }
            }
        }
    }

    void CalDirection()
    {
        Vector3 resultDirection = new Vector3(
        transform.rotation.eulerAngles.x + Random.Range(1-currentAccuracy, currentAccuracy-1) * 20f,
        transform.rotation.eulerAngles.y + Random.Range(1-currentAccuracy, currentAccuracy-1) * 20f, 0);

        Debug.Log(currentAccuracy);
        raycastData.muzzle.rotation = Quaternion.Euler(resultDirection);
    }
}
