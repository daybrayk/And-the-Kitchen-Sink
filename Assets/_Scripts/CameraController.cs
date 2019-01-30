using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraController : MonoBehaviour {
    public PlayerController pc;

    [SerializeField]
    private LayerMask uiMask;
	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        /*float vValue = Input.GetAxis("Mouse Y");
        
        transform.eulerAngles = new Vector3(transform.eulerAngles.x - vValue, transform.eulerAngles.y, transform.eulerAngles.z);*/
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 500f, uiMask))
        {
            pc.isFacingUI = true;
        }
        else
        {
            pc.isFacingUI = false;
        }
	}
}
