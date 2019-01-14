using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RaycastProjectileData : WeaponData{
    public GameObject bullet;
    public float range;
    public float force;
    public Vector3 windResistance;
}

public class RaycastProjectileWeapons : Weapon {
    public RaycastProjectileData projectileData;
    protected override void Start()
    {
        data = projectileData;
        base.Start();   
    }

    protected override void LaunchProjectile()
    {
        //CalDirection();
        BulletProjectile projectile = Instantiate(projectileData.bullet, projectileData.muzzle.position, transform.rotation).GetComponent<BulletProjectile>();
        projectile.Launch(projectileData.damage, projectileData.force, projectileData.range, projectileData.windResistance);
    }

    void CalDirection()
    {
        Vector3 resultDirection = new Vector3(
        transform.rotation.eulerAngles.x + Random.Range(1 - currentAccuracy, currentAccuracy - 1) * 20f,
        transform.rotation.eulerAngles.y + Random.Range(1 - currentAccuracy, currentAccuracy - 1) * 20f, 0);

        Debug.Log(currentAccuracy);
        projectileData.muzzle.rotation = Quaternion.Euler(resultDirection);
    }

}
