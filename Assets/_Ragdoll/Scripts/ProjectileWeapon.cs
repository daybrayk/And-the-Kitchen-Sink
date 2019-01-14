using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectileWeaponData : WeaponData
{
    public GameObject projectile;
    public float force;
}

public class ProjectileWeapon : Weapon {

    public ProjectileWeaponData projectileData;

    protected override void Start()
    {
        data = projectileData;
        base.Start();
    }

    // Write ProjectileLaunch Script here
    protected override void LaunchProjectile()
    {
        Projectile projectile = Instantiate(projectileData.projectile, projectileData.muzzle).GetComponent<Projectile>();
        projectile.Launch(projectileData.damage, projectileData.force);

    }
}
