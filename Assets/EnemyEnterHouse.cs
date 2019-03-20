using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnterHouse : MonoBehaviour {
    public GameManager gm;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" || other.tag == "Torso")
        {
            gm.GameOver();
        }
    }
}
