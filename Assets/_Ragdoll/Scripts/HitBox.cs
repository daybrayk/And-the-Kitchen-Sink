using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox: MonoBehaviour
{
    public float damageMultiplier;
    public Neighbour myPlayer;
    public float hitVisualTime = 0.1f;

    private bool hit;
    public float hitTimer;

    public bool Hit(float damage, float x = 0, float y = 0, float z = 0)
    {
        Vector3 forceDir = new Vector3(x, y, z);
        bool iskilled = myPlayer.TakeDamage(damage * damageMultiplier,forceDir);
        hit = true;
        hitTimer = hitVisualTime;
        GetComponent<Renderer>().material.SetColor("_Color", Color.red);

        if(iskilled)
        {
            Destroy(this);
        }

        return iskilled;
    }

    public bool Hit(float damage, Vector3 force)
    {
        bool iskilled = myPlayer.TakeDamage(damage * damageMultiplier, force);
        hit = true;
        hitTimer = hitVisualTime;
        GetComponent<Renderer>().material.SetColor("_Color", Color.red);

        if (iskilled)
        {
            Destroy(this);
        }

        return iskilled;
    }

    private void Start()
    {
        //Material newMaterial = new Material(Shader.Find("Custom/ChangeColor"));
        //GetComponent<Renderer>().material = newMaterial;
    }

    void Update()
    {
        if(hit)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer <= 0)
            {
                GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                hit = false;
            }
        }
    }
}
