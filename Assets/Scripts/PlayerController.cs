using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    #region Public Variables
    public Transform sinkSpawn;
    public Transform playerCam;
    public LayerMask trajectoryMask;
    #endregion

    #region Private Variables
    [SerializeField]
    float powerLimit;

    [SerializeField]
    float powerMin;
    float _timer;
    float throwPower;

    [SerializeField]
    float _sinkCD;
    GameObject _sinkInHands;

    [SerializeField]
    List<GameObject> _sinks = new List<GameObject>();
    SinkController sinkScript;
    LineRenderer lr;
    #endregion

    // Use this for initialization
    void Awake () {
        _timer = _sinkCD;
        lr = GetComponent<LineRenderer>();
        if (powerMin <= 0)
            powerMin = 10f;
        throwPower = powerMin;
	}
    private void Start()
    {
        SpawnSink();
    }

    // Update is called once per frame
    void Update () {
        float hValue = Input.GetAxisRaw("Mouse X");
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + hValue, transform.eulerAngles.z);
        if(sinkInHands == null)
        {
            _timer -= Time.deltaTime;
            if (_timer < 0)
            {
                SpawnSink();
                _timer = _sinkCD;
            }
        }else if (Input.GetMouseButton(0))
        {
            if (throwPower <= powerLimit)
                throwPower += powerLimit * Time.deltaTime;
            else
                throwPower = powerLimit;
            SinkTrajectory();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ThrowSink();
            throwPower = powerMin;
        }
        else
        {
            SinkTrajectory();
        }
    }

    private void FixedUpdate()
    {
        //SinkTrajectory();
    }

    private void SinkTrajectory()
    {
        float t = 0.02f;
        RaycastHit hit;
        lr.enabled = false;
        Vector3 origin = sinkInHands.transform.position;
        Vector3 nextPos = origin;
        Vector3 currentPos = origin;
        lr.positionCount = 1;
        lr.SetPosition(0, currentPos);
        while (!Physics.Linecast(currentPos, nextPos, out hit, trajectoryMask) && t < 3.0f)
        {
            currentPos = nextPos;
            //nextPos = origin + playerCam.forward * (throwPower * t + (0.5f * (-9.8f) * Mathf.Pow(t, 2)));
            float x = origin.x + sinkScript.vx;
            float y = origin.y + (sinkScript.vy + ((Physics.gravity.y * t) / 2) * t);
            float z = origin.z + sinkScript.vz;
            nextPos = new Vector3(x, y, z);
            lr.positionCount++;
            lr.SetPosition(lr.positionCount-1, nextPos);
            t += Time.fixedDeltaTime;
            //Debug.Log(t);
        }
        lr.enabled = true;
        //if t < 3.0f then the while loop boke because the raycast hit something
        if(t<3.0f)
        {

        }
    }

    private void SpawnSink()
    {
        sinkInHands = Instantiate(_sinks[Random.Range(0, _sinks.Capacity)], sinkSpawn);
        sinkInHands.transform.position = sinkSpawn.position;
        sinkScript = sinkInHands.GetComponent<SinkController>();
        sinkScript.sinkSpawn = sinkSpawn;
        sinkScript.AdjustForce(throwPower);
    }

    private void ThrowSink()
    {
        sinkScript.Throw(playerCam.forward * throwPower);
        sinkInHands = null;
        sinkScript = null;
        lr.enabled = false;
    }

    #region Getters and Setters
    public GameObject sinkInHands
    {
        get { return _sinkInHands; }
        set { _sinkInHands = value; }
    }

    public float sinkCD
    {
        get { return _sinkCD; }
        set { _sinkCD = value;
            _timer = _sinkCD; }
    }
    #endregion
}
