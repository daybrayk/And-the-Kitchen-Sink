using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData 
{
    public string name = "GUN";
    public Transform muzzle;
    public float damage;
    public float fireRate;
    [Range(0.0f, 1.0f)]
    public float accuracy;
    [Range(0.0f, 1.0f)]
    public float accuracyMultiplierPerShot;
    [HideInInspector]
    public float accuracyMultiplier = 1;
    public int magazineSize;
    //[HideInInspector]
    public int myPlayerLayer;
    public float reloadTime;
    public float accuracyResetRate;
}

public class Weapon : MonoBehaviour {

    protected WeaponData data;
    private bool firing = false;
    [SerializeField]
    private bool reloading = false;
    private int bulletsRemaining;
    protected float currentAccuracy;
    private float fireWaitTime = 0f;

	// Use this for initialization
	protected virtual void Start ()
    {
        bulletsRemaining = data.magazineSize;
        ResetAccuracy();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(fireWaitTime > 0)
        {
            fireWaitTime -= Time.deltaTime;
        }
		if (firing)
        {
            if (bulletsRemaining > 0)
            {
                if (fireWaitTime <= 0)
                {
                    Shoot();
                }
            }
            else
            {
                Reload();
            }
        }
        else
        {
            if (currentAccuracy < data.accuracy * data.accuracyMultiplier)
            {
                currentAccuracy += data.accuracyResetRate * Time.deltaTime;
                if (currentAccuracy > data.accuracy * data.accuracyMultiplier)
                {
                    ResetAccuracy();
                }
            }
        }
	}

    public void Fire()
    {
        if (!reloading)
        {
            firing = true;
        }
    }

    public void StopFiring()
    {
        firing = false;
    }

    public float GetCurrentAccuracy()
    {
        return currentAccuracy;
    }

    public int GetBulletsRemaining()
    {
        return bulletsRemaining;
    }

    public void Reload()
    {
        if (!reloading)
        {
            StartCoroutine(StartReload());
        }
    }

    protected virtual void LaunchProjectile(){}

    private void Shoot()
    { 
        LaunchProjectile();
        bulletsRemaining--;
        currentAccuracy *= data.accuracyMultiplierPerShot;
        fireWaitTime = 1 / data.fireRate;
    }

    private IEnumerator StartReload()
    {
        reloading = true;
        yield return new WaitForSeconds(data.reloadTime);
        bulletsRemaining = data.magazineSize;
        ResetAccuracy();
        reloading = false;
        yield return null;
    }
    private void ResetAccuracy()
    {
        currentAccuracy = data.accuracy * data.accuracyMultiplier;
    }
}
