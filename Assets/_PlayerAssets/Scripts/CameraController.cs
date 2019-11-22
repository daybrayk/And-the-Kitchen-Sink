using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraController : MonoBehaviour {
    public PlayerController pc;

    [SerializeField] private LayerMask m_uiMask;
    
	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        
        if(Physics.Raycast(transform.position, transform.forward, out hit, 500f, m_uiMask))
        {
            pc.isFacingUI = true;
            
        }
        else
        {
            pc.isFacingUI = false;
        }
	}
}
