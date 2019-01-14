using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTrajectory : MonoBehaviour {
    public LayerMask trajectoryMask;
    public LineRenderer lr;
    SinkController sinkScript;
    private void Start()
    {
        if(!sinkScript)
            sinkScript = GetComponent<SinkController>();
        if (!lr)
            lr = GetComponent<LineRenderer>();
    }
    private void FixedUpdate()
    {
        if (lr.enabled == true)
            SinkTrajectory();
    }

    private void SinkTrajectory()
    {
        float t = 0.02f;
        RaycastHit hit;
        lr.enabled = false;
        Vector3 origin = transform.position;
        Vector3 nextPos = origin;
        Vector3 currentPos = origin;
        lr.positionCount = 1;
        lr.SetPosition(0, currentPos);
        while (!Physics.Linecast(currentPos, nextPos, out hit, trajectoryMask) && t < 3.0f)
        {
            currentPos = nextPos;
            //nextPos = origin + playerCam.forward * (throwPower * t + (0.5f * (-9.8f) * Mathf.Pow(t, 2)));
            float x = origin.x + sinkScript.vx * t;
            float y = origin.y + (sinkScript.vy*t + ((Physics.gravity.y * t) / 2) * t);
            float z = origin.z + sinkScript.vz * t;
            nextPos = new Vector3(x, y, z);
            lr.positionCount++;
            lr.SetPosition(lr.positionCount - 1, nextPos);
            t += Time.fixedDeltaTime;
            //Debug.Log(t);
        }
        lr.enabled = true;
        //if t < 3.0f then the while loop boke because the raycast hit something
        if (t < 3.0f)
        {

        }
    }
}
