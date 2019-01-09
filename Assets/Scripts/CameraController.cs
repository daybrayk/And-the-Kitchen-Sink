using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        float vValue = Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(transform.eulerAngles.x - vValue, transform.eulerAngles.y, transform.eulerAngles.z);
	}
}
