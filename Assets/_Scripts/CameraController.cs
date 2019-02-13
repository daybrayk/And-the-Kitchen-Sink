using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraController : MonoBehaviour {
    public PlayerController pc;

    [SerializeField] private LayerMask m_uiMask;
    [SerializeField] private LayerMask m_bunkerMask;
    
	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        /*float vValue = Input.GetAxis("Mouse Y");
        
        transform.eulerAngles = new Vector3(transform.eulerAngles.x - vValue, transform.eulerAngles.y, transform.eulerAngles.z);*/
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 500f, m_uiMask))
        {
            pc.isFacingUI = true;
        }
        else if(Physics.Raycast(transform.position, transform.forward, out hit, 500f, m_bunkerMask))
        {
            pc.isFacingUI = true;
            if (Input.GetMouseButtonDown(0))
                hit.transform.gameObject.GetComponent<BunkerScript>().ChangeBunker();
            
        }
        else
        {
            pc.isFacingUI = false;
        }
	}
}
