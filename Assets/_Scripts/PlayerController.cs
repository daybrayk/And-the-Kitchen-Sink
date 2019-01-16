using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour {
    #region Public Variables
    public Transform sinkSpawn;
    public Transform playerCam;
    public LayerMask trajectoryMask;
    public bool isFacingUI;
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
    #endregion

    // Use this for initialization
    void Awake () {
        _timer = _sinkCD;
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
#if UNITY_EDITOR
        float hValue = Input.GetAxisRaw("Mouse X");
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + hValue, transform.eulerAngles.z);  
#endif
        if (!isFacingUI)
        {
            if (sinkInHands == null)
            {
                _timer -= Time.deltaTime;
                if (_timer < 0)
                {
                    SpawnSink();
                    _timer = _sinkCD;
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if (throwPower <= powerLimit)
                    throwPower += powerLimit * Time.deltaTime;
                else
                    throwPower = powerLimit;
                sinkScript.AdjustForce(throwPower);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ThrowSink();
                throwPower = powerMin;
            }
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
