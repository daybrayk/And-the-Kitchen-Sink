using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour {
    #region Public Variables
    public Transform sinkSpawn;
    [HideInInspector]
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
    [SerializeField]
    private GameManager gm;
    private BunkerScript m_currentBunker;
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
/*#if UNITY_EDITOR
        float hValue = Input.GetAxisRaw("Mouse X");
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + hValue, transform.eulerAngles.z);
#endif*/
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
                sinkScript.force = throwPower;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ThrowSink();
                throwPower = powerMin;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
            ChangeSink();
    }

    private void SpawnSink()
    {
        //sinkInHands = Instantiate(_sinks[Random.Range(0, _sinks.Capacity)], sinkSpawn);
        sinkInHands = Instantiate(_sinks[Random.Range(0, 3)], sinkSpawn);
        sinkInHands.transform.position = sinkSpawn.position;
        sinkScript = sinkInHands.GetComponent<SinkController>();
        sinkScript.SetGM(gm);
        gm.AddSink(sinkInHands);
        Debug.Assert(sinkScript, "Variable sinkScript in PlayerController is NULL!");
        sinkScript.sinkSpawn = sinkSpawn;
        sinkScript.force = throwPower;
    }

    private void ThrowSink()
    {
        sinkScript.Throw(throwPower);
        sinkInHands = null;
        sinkScript = null;
    }

    private void ChangeSink()
    {
        if (currentBunker.m_storedSink)  //If a sink is already stored here then swap with the sink currently in the players hands
        {
            sinkInHands = currentBunker.SwapSink(sinkInHands);
            sinkScript = sinkInHands.GetComponent<SinkController>();
        }
        else   //If no sink is stored in this bunker then store the sink currently in the player's hands and spawn a new sink
        {
            currentBunker.StoreSink(sinkInHands);
            SpawnSink();
        }
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

    public BunkerScript currentBunker
    {
        get { return m_currentBunker; }
        set { m_currentBunker = value; }
    }
#endregion
}
